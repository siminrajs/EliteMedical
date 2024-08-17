using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.Repository;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        HMSAPIRespository hmsAPIRespository;
        private readonly IConfiguration _config;
        string BaseUrl = string.Empty;
        public PatientDetailsController(IConfiguration configuration)
        {
            _config = configuration;
            BaseUrl = _config["HMSApiBaseUrl"];
        }

        [HttpPost]
        [Route("GetPatientDetailsByPatId/")]
        public ResponseWrapper<Profile> GetPatientDetailsByPatId([FromHeader(Name = "username")] string username,
        [FromHeader(Name = "password")] string password,
        [FromBody] APIInputs inputs)
        {
            ResponseWrapper<Profile> result = new ResponseWrapper<Profile>();
            result.Result = new Result<Profile>();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                var data = hmsAPIRespository.GetPatientDetailsForAPI(inputs.PatId);
                return data;
            }
            catch (Exception ex)
            {
                result.Result.IsSuccess = false;
                result.Result.Message = ex.Message;
            }
            return result;
        }
    }
}
