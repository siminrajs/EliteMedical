using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.HMSAPIHelpers;
using EliteHospital.Data.Repository;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Data;
using Newtonsoft.Json;

namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly IConfiguration _config;
        string BaseUrl = string.Empty;
        public LoyaltyController(IConfiguration configuration)
        {
            _config = configuration;
            BaseUrl = _config["HMSApiBaseUrl"];
        }
        [HttpGet]
        [Route("GetPatientdetailid/")]
        public LoyaltyPointList<TransactionViewModel> GetPatientdetailid(string Id)
        {
            LoyaltyPointList<TransactionViewModel> result = new LoyaltyPointList<TransactionViewModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@PatientId", Id));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetTransactionList", sqlParams);
                    string Name = "";
                    decimal BalancePoints = 0;
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<TransactionViewModel> result1 = new List<TransactionViewModel>();
                        result1 = db.ConvertDataTableToListClass<TransactionViewModel>(dsResult.Tables[1]).ToList();
                        Name = dsResult.Tables[0].Rows[0]["Name"].ToString();
                        BalancePoints = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
                        result.Data = result1;                       
                    }          
                    result.IsSuccess = true;
                    result.Name = Name;
                    result.BalancePoint = BalancePoints;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("GetLoyaltyPointList/")]
        public ServicePointList<ServiceListClass> GetLoyaltyPointList(string Id)
        {
            ServicePointList<ServiceListClass> result = new ServicePointList<ServiceListClass>();
            ServicePointList<ServiceListClass> trData = new ServicePointList<ServiceListClass>();
            List<TransactionViewModel> Transcationlist = new List<TransactionViewModel>();
            List<ServiceModel> Servicelist = new List<ServiceModel>();
           
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@PatientId", Id));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetLoyaltyPointList", sqlParams);
                    decimal BalancePoints = 0;decimal MiniRedeemPoint1 = 0;
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<TransactionViewModel> result1 = new List<TransactionViewModel>();
                        result1 = db.ConvertDataTableToListClass<TransactionViewModel>(dsResult.Tables[1]).ToList();
                        List<ServiceModel> servicedetails = new List<ServiceModel>();
                        servicedetails = db.ConvertDataTableToListClass<ServiceModel>(dsResult.Tables[2]).ToList();                 
                        BalancePoints = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
                        MiniRedeemPoint1 = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["MiniRedeemPoint"].ToString());
                        Transcationlist = result1;
                        Servicelist = servicedetails;
                        ServiceListClass data = new ServiceListClass();
                        data.Servicelist = Servicelist;
                        data.Transcationlist = Transcationlist;
                        result.data = data;                          
                    }

                    result.IsSuccess = true;
                    result.Name = "";
                    result.BalancePoint = BalancePoints;
                    result.Message = "success";
                    result.MiniRedeemPoint = MiniRedeemPoint1;
                }
               
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
      
        [HttpGet]
        [Route("RedeemPoint/")]
        public ResponseDataPromocode<TransactionViewModel> RedeemPoint(string PatientId, string ServiceId,string TransactionId)
         {
            ResponseDataPromocode<TransactionViewModel > result = new ResponseDataPromocode<TransactionViewModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    //Generate Promocode
                        //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                        string numbers = "1234567890";
                        string characters = "";
                        characters = characters  + numbers;

                        int length = 10;
                        string Promocode = "EMC";
                        for (int i = 0; i < length; i++)
                        {
                            string character = string.Empty;
                            do
                            {
                                int index = new Random().Next(0, characters.Length);
                                character = characters.ToCharArray()[index].ToString();
                            } while (Promocode.IndexOf(character) != -1);
                        Promocode += character;
                        }
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@PatientId", Convert.ToInt64(PatientId)));
                    sqlParams.Add(new SqlParameter("@Promocode", Promocode));
                    sqlParams.Add(new SqlParameter("@ServiceId", ServiceId)); 
                    sqlParams.Add(new SqlParameter("@TransactionId",Convert.ToInt64(TransactionId)));
                    DataSet dsResult = db.SqlDataSetResult("USP_UpdatePromocode", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        result.PatientId = Convert.ToInt64(dsResult.Tables[0].Rows[0]["PatientId"].ToString());
                        result.Promocode = dsResult.Tables[0].Rows[0]["Promocode"].ToString();
                        result.ServiceId = Convert.ToInt64(dsResult.Tables[0].Rows[0]["ServiceId"].ToString());
                        result.BalancePoints = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("GetTermsandConditions/")]
        public ResponseTermsAndConditions<TermsAndConditionsViewModel> GetTermsandConditions()
        {
            ResponseTermsAndConditions<TermsAndConditionsViewModel> result = new ResponseTermsAndConditions<TermsAndConditionsViewModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    DataSet dsResult = db.SqlDataSetResult("USP_GetTermsandConditionsList", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<TermsAndConditionsViewModel> list= new List<TermsAndConditionsViewModel>();
                        list = db.ConvertDataTableToListClass<TermsAndConditionsViewModel>(dsResult.Tables[0]).ToList();
                        result.Data = list;
                        result.IsSuccess = true;
                        result.Message = "success";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #region Models
        public class LoyaltyPointList<T> : ResponseData
        {
            public List<T> Data { get; set; }
        }

        public class ResponseData
        {
            public bool IsSuccess { get; set; }
            public string Name { get; set; }
            public decimal BalancePoint { get; set; }
            public string Message { get; set; }
        }

        public class ResponseDataPromocode<T> 
        {
            public Int64 PatientId { get; set; }
            public Int64 ServiceId { get; set; }
            public string Promocode { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public decimal BalancePoints { get; set; }
        }

        public class ResponseTermsAndConditions<T> : Responsetermsandconditionlist
        {
            public List<T> Data { get; set; }
        }
        public class Responsetermsandconditionlist
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }


        #endregion Models
    }
}
