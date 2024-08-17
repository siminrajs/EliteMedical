using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSAboutusRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSAboutusRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<AboutU> GetAll()
        {
            try
            {
                return context.AboutUS.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AboutU GetById(int Id)
        {
            try
            {
                return context.AboutUS.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(AboutU aboutus)
        {
            try
            {
                AboutU aboutusToUpdate = context.AboutUS.Where(p => p.Id == aboutus.Id).FirstOrDefault();
                if (aboutusToUpdate != null)
                {
                    aboutusToUpdate.ShortDescription = aboutus.ShortDescription;
                    aboutusToUpdate.OurVision = aboutus.OurVision;
                    aboutusToUpdate.OurMission = aboutus.OurMission;
                    aboutusToUpdate.LongDescription = aboutus.LongDescription;
                    if (aboutus.Image != null && aboutus.Image.Length > 0)
                    {
                        aboutusToUpdate.Image = aboutus.Image;
                        aboutusToUpdate.ImagePath = aboutus.ImagePath;
                    }
                    context.SaveChanges();
                }
                else
                {
                    context.AboutUS.Add(aboutus);
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
                AboutU aboutus = context.AboutUS.Where(p => p.Id == Id).FirstOrDefault();
                if (aboutus != null)
                {
                    context.AboutUS.Remove(aboutus);
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
