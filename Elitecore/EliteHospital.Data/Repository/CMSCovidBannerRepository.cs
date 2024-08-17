using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSCovidBannerRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSCovidBannerRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<CovidBanner> GetAll()
        {
            try
            {
                return context.CovidBanners.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CovidBanner GetById(int Id)
        {
            try
            {
                return context.CovidBanners.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(CovidBanner covidbanner)
        {
            try
            {
                context.CovidBanners.Add(covidbanner);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(CovidBanner covidbanner)
        {
            try
            {
                CovidBanner covidbannerToUpdate = context.CovidBanners.Where(p => p.Id == covidbanner.Id).FirstOrDefault();
                if (covidbannerToUpdate != null)
                {
                    covidbannerToUpdate.Name = covidbanner.Name;
                    if (covidbanner.Image != null && covidbanner.Image.Length > 0)
                    {
                        covidbannerToUpdate.Image = covidbanner.Image;
                    }
                    covidbannerToUpdate.ImagePath = covidbanner.ImagePath;
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
                CovidBanner covidbanner = context.CovidBanners.Where(p => p.Id == Id).FirstOrDefault();
                if (covidbanner != null)
                {
                    context.CovidBanners.Remove(covidbanner);
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
