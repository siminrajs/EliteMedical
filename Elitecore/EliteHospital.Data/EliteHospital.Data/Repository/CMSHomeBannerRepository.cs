using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSHomeBannerRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSHomeBannerRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<HomeBanner> GetAll()
        {
            try
            {
                return context.HomeBanners.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HomeBanner GetById(int Id)
        {
            try
            {
                return context.HomeBanners.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(HomeBanner homeBanner)
        {
            try
            {
                HomeBanner homeBannerToUpdate = context.HomeBanners.Where(p => p.Id == homeBanner.Id).FirstOrDefault();
                if (homeBannerToUpdate != null)
                {
                    homeBannerToUpdate.Title = homeBanner.Title;
                    homeBannerToUpdate.Description = homeBanner.Description;
                    homeBannerToUpdate.Description = homeBanner.Description;
                    homeBannerToUpdate.ExploreUrl = homeBanner.ExploreUrl;
                    if (homeBannerToUpdate.Image != null && homeBanner.Image.Length > 0)
                    {
                        homeBannerToUpdate.Image = homeBanner.Image;
                        homeBannerToUpdate.ImagePath = homeBanner.ImagePath;
                    }
                    context.SaveChanges();
                }
                else
                {
                    context.HomeBanners.Add(homeBanner);
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
                HomeBanner  homeBanner = context.HomeBanners.Where(p => p.Id == Id).FirstOrDefault();
                if (homeBanner != null)
                {
                    context.HomeBanners.Remove(homeBanner);
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
