using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSConfigPasswordsRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSConfigPasswordsRepository()
        {
            context = new EliteHospitalEntities();
        }
        public List<ConfigPasswords> GetAll()
        {
            try
            {
                return context.ConfigPasswords.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfigPasswords GetById(int Id)
        {
            try
            {
                return context.ConfigPasswords.Where(p => p.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(ConfigPasswords configpass)
        {
            try
            {
                context.ConfigPasswords.Add(configpass);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(ConfigPasswords configpass)
        {
            try
            {
                ConfigPasswords configpassToUpdate = context.ConfigPasswords.Where(p => p.ID == configpass.ID).FirstOrDefault();
                if (configpassToUpdate != null)
                {
                    configpassToUpdate.ConfigPasswordName  = configpass.ConfigPasswordName;
                    configpassToUpdate.ConfigPassValues = configpass.ConfigPassValues;
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(int Id)
        {
            try
            {
                ConfigPasswords configpass = context.ConfigPasswords.Where(p => p.ID == Id).FirstOrDefault();
                if (configpass != null)
                {
                    context.ConfigPasswords.Remove(configpass);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
