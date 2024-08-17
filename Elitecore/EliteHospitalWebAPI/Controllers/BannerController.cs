using EliteHospital.Data;
using EliteHospital.Data.Repository;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly EliteHospitalContext context;
        string BaseUrl = string.Empty;

        public BannerController(IConfiguration configuration, EliteHospitalContext _context)
        {
            config = configuration;
            context = _context;
            BaseUrl = config["HMSApiBaseUrl"];
        }

        [HttpPost]
        [Route("GetAllBanners/")]
        public Result<BannerViewModel> GetAllBanners([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<BannerViewModel> result = new Result<BannerViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<Banner> banners = context.Banners.ToList();

                result.Data = banners.Select(p => new BannerViewModel()
                {
                    Id = p.Id,
                    BannerTitle = p.BannerTitle,
                    BannerTitleArabic = p.BannerTitleArabic,
                    BannerSubTitle = p.BannerSubTitle,
                    BannerSubTitleArabic = p.BannerSubTitleArabic,
                    BannerImage = p.BannerImage,
                    BannerImageMobile = p.BannerImageMobile
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Banners fetched successfully";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("GetAllCovidBanners/")]
        public Result<CovidBannerViewModel> GetAllCovidBanners([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<CovidBannerViewModel> result = new Result<CovidBannerViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<CovidBanner> banners = context.CovidBanners.ToList();

                result.Data = banners.Select(p => new CovidBannerViewModel()
                {
                    Id = p.Id,
                    Title = p.Name,
                    Image = p.Image
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Covid Banners fetched successfully";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
