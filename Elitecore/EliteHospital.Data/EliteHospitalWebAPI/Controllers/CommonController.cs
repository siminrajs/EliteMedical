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
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly EliteHospitalContext context;
        string BaseUrl = string.Empty;

        public CommonController(IConfiguration configuration, EliteHospitalContext _context)
        {
            config = configuration;
            context = _context;
            BaseUrl = config["HMSApiBaseUrl"];
        }

        [HttpPost]
        [Route("GetSpecialOffers/")]
        public Result<SpecialOfferViewModel> GetSpecialOffers([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<SpecialOfferViewModel> result = new Result<SpecialOfferViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<SpecialOffer> specialoffers = context.SpecialOffers.ToList();

                result.Data = specialoffers.Select(p => new SpecialOfferViewModel()
                {
                    Id = p.Id,
                    Image = p.Image,
                    ImageMob = p.ImageMob
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Special Offers fetched successfully";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("GetInsurances/")]
        public Result<InsuranceViewModel> GetInsurances([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<InsuranceViewModel> result = new Result<InsuranceViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<Insurance> insurances = context.Insurance.ToList();

                result.Data = insurances.Select(p => new InsuranceViewModel()
                {
                    Id = p.Id,
                    Image = p.Image,
                    ImageMob = p.ImageMob
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Insurances fetched successfully";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("GetDailyOffers/")]
        public Result<DailyOffersViewModel> GetDailyOffers([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<DailyOffersViewModel> result = new Result<DailyOffersViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<DailyOffer> dailyOffers = context.DailyOffer.ToList();

                result.Data = dailyOffers.Select(p => new DailyOffersViewModel()
                {
                    Id = p.Id,
                    Image = p.Image,
                    ImageMob = p.ImageMob
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Daily Offers fetched successfully";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("GetContactUs/")]
        public Result<ContactUsViewModel> GetContactUs([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<ContactUsViewModel> result = new Result<ContactUsViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<ContactU> contactus = context.ContactUs.ToList();

                result.Data = contactus.Select(p => new ContactUsViewModel()
                {
                    Id = p.Id,
                    Address = p.Address,
                    Email = p.Email,
                    Phone = p.Phone,
                    WorkingHours = p.WorkingHours,
                    Location = p.Location,
                    WorkingHoursMob = p.WorkingHoursMob,
                    AddressMob = p.AddressMob,
                    WorkingHoursArabicMob = p.WorkingHoursArabicMob,
                    AddressArabicMob = p.AddressArabicMob
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Contact Us fetched successfully";
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
