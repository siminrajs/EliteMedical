using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EliteHospital.Data.Repository
{
    public class CMSBannerRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSBannerRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<Banner> GetAll()
        {
            try
            {
                return context.Banners.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Banner GetById(int Id)
        {
            try
            {
                return context.Banners.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Banner banner)
        {
            try
            {
                context.Banners.Add(banner);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Banner banner)
        {
            try
            {
                Banner bannerToUpdate = context.Banners.Where(p => p.Id == banner.Id).FirstOrDefault();
                if (bannerToUpdate != null)
                {
                    bannerToUpdate.BannerTitle = banner.BannerTitle;
                    bannerToUpdate.BannerSubTitle = banner.BannerSubTitle;
                    bannerToUpdate.BannerTitleArabic = banner.BannerTitleArabic;
                    bannerToUpdate.BannerSubTitleArabic = banner.BannerSubTitleArabic;
                    if (banner.BannerImage != null && banner.BannerImage.Length > 0)
                    {
                        bannerToUpdate.BannerImage = banner.BannerImage;
                    }
                    if (banner.BannerImageMobile != null && banner.BannerImageMobile.Length > 0)
                    {
                        bannerToUpdate.BannerImageMobile = banner.BannerImageMobile;
                    }
                    if (!string.IsNullOrEmpty(banner.DepartmentName))
                    {
                        bannerToUpdate.DepartmentName = banner.DepartmentName;
                    }
                    bannerToUpdate.BannerImagePath = banner.BannerImagePath;
                    bannerToUpdate.BannerImageMobilePath = banner.BannerImageMobilePath;
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
                Banner banner = context.Banners.Where(p => p.Id == Id).FirstOrDefault();
                if (banner != null)
                {
                    context.Banners.Remove(banner);
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
