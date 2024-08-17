using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSInsuranceRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSInsuranceRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<Insurance> GetAll()
        {
            try
            {
                return context.Insurances.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Insurance GetById(int Id)
        {
            try
            {
                return context.Insurances.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Insurance insurance)
        {
            try
            {
                Insurance insuranceToUpdate = context.Insurances.Where(p => p.Id == insurance.Id).FirstOrDefault();
                if (insuranceToUpdate != null)
                {
                    if (insurance.Image != null && insurance.Image.Length > 0)
                    {
                        insuranceToUpdate.Image = insurance.Image;
                        insuranceToUpdate.ImagePath = insurance.ImagePath;
                    }
                    if (insurance.ImageMob != null && insurance.ImageMob.Length > 0)
                    {
                        insuranceToUpdate.ImageMob = insurance.ImageMob;
                        insuranceToUpdate.ImageMobPath = insurance.ImageMobPath;
                    }
                    context.SaveChanges();
                }
                else
                {
                    context.Insurances.Add(insurance);
                    context.SaveChanges();
                }
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
                Insurance insurance = context.Insurances.Where(p => p.Id == Id).FirstOrDefault();
                if (insurance != null)
                {
                    context.Insurances.Remove(insurance);
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
