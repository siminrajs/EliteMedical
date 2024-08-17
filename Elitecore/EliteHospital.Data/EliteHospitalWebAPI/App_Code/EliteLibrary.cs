using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;


namespace EliteHospitalWebAPI
{
    public class EliteLibrary
    {
        public static String SqlConnectionString = "";

        public class DAL : IDisposable
        {
            public DAL(IConfiguration configuration)
            {
                SqlConnectionString = configuration.GetConnectionString("EliteHospitalContext");
            }

            public SqlConnection? ClientSqlCon { get; set; }
            void IDisposable.Dispose()
            {
                if (ClientSqlCon != null)
                    ClientSqlCon.Dispose();
            }            
           public DataSet SqlDataSetResult(String storedProcedureName, List<SqlParameter> sqlParams, Boolean isStoredProcedure = true)
            {
                DataSet ds = new DataSet();
                using (ClientSqlCon = new SqlConnection(SqlConnectionString))
                {
                    SqlTransaction transaction;
                    ClientSqlCon.Open();
                    transaction = ClientSqlCon.BeginTransaction();
                    try
                    {
                        SqlCommand clientSqlCmd = new SqlCommand(storedProcedureName, ClientSqlCon, transaction)
                        {
                            CommandType = (isStoredProcedure) ? CommandType.StoredProcedure : CommandType.Text,
                            CommandTimeout = 0
                        };

                        if (sqlParams.Count > 0)
                        {
                            clientSqlCmd.Parameters.AddRange(sqlParams.ToArray());
                        }

                        SqlDataAdapter da = new SqlDataAdapter(clientSqlCmd);
                        da.Fill(ds);
                        transaction.Commit();
                        ClientSqlCon.Close();
                    }
                    catch (Exception ex)
                    {                       
                        transaction.Rollback();
                        ClientSqlCon.Close();
                    }
                }
                return ds;
            }

            public List<T> ConvertDataTableToListClass<T>(DataTable dt)
            {
                List<T> data = new List<T>();
                try
                {
                    data = dt.AsEnumerable().Select(row => GetItem<T>(row)).ToList();
                }
                catch { data = new List<T>(); }
                return data;
            }



            public T ConvertDataTableToClass<T>(DataTable dt) where T : new()
            {
                T item = new T();
                try
                {
                    DataRow row = dt.Rows[0];
                    // set the item
                    SetItemFromRow(item, row);
                }
                catch { }
                return item;
            }

            #region Private Methods
           
            private void SetItemFromRow<T>(T item, DataRow row) where T : new()
            {
                foreach (DataColumn c in row.Table.Columns)
                {
                    PropertyInfo? p = item.GetType().GetProperty(c.ColumnName);
                    if (p != null && row[c] != DBNull.Value)
                    {
                        p.SetValue(item, row[c], null);
                    }
                }
            }
            private T GetItem<T>(DataRow dr)
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in dr.Table.Columns)
                {
                    PropertyInfo? pro = temp.GetProperties().Where(t => t.Name == column.ColumnName).FirstOrDefault();
                    if (pro != null)
                    {
                        var cellValue = dr[column.ColumnName];
                        cellValue = (cellValue == DBNull.Value) ? "" : cellValue;
                        pro.SetValue(obj, cellValue, null);
                    }
                }
                return obj;
            }
            #endregion Private Methods
        }       
    }
}
