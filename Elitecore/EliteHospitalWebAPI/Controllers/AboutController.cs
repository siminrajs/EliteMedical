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
    public class AboutController : ControllerBase 
    {
        private readonly IConfiguration config;
        private readonly EliteHospitalContext context;
        string BaseUrl = string.Empty;
        public AboutController(IConfiguration configuration, EliteHospitalContext _context)
        {
            config = configuration;
            context = _context;
            BaseUrl = config["HMSApiBaseUrl"];
        }
        [HttpGet]
        [Route("GetAboutus/")]
        public Result<AboutUsViewModel> GetAboutus([FromHeader(Name = "username")] string username,
           [FromHeader(Name = "password")] string password)
        {
            Result<AboutUsViewModel> result = new Result<AboutUsViewModel>();
            try
            {
                if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                {
                    result.IsSuccess = false;
                    result.Message = "The remote server returned an error: (401) Unauthorized.";
                    return result;
                }

                List<AboutU > aboutu = context.AboutUS .ToList();

                result.Data = aboutu.Select(p => new AboutUsViewModel()
                {
                    Id = p.Id,
                    ShortDescription = p.ShortDescription,
                    OurVision = p.OurVision,
                    OurMission = p.OurMission,
                    LongDescription = p.LongDescription ,
                    Image = p.ImagePath 
                }).ToList();
                result.IsSuccess = true;
                result.Message = "Aboutus fetched successfully";
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
