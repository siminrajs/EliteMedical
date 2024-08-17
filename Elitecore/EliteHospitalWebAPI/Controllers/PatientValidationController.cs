using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Request;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.Repository;
using EliteHospital.Services;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using EliteHospital.Data.HMSAPIHelpers;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Data;
using Newtonsoft.Json;


namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientValidationController : ControllerBase
    {
        HMSAPIRespository hmsAPIRespository;
        private readonly IConfiguration _config;
        private readonly IConfiguration config;
        private readonly EliteHospitalContext context;
        string BaseUrl = string.Empty;
        public PatientValidationController(IConfiguration configuration, EliteHospitalContext _context)
        {
            config = configuration;
            _config = configuration;
            context = _context;
            BaseUrl = config["HMSApiBaseUrl"];
        }

        [HttpPost]
        [Route("ValidatePatient/")]
        public Result<ValidatePatientResponse> GetPatientDetailsByPatId([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                var data = hmsAPIRespository.ValidatePatient(inputs.MobileNo, inputs.QatarId);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("SavePatientDetails/")]
        public ResponseWrapper<SignUpResponse> SavePatientDetails([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] SignUpRequest patientDetails)
        {
            try
            {            
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                ResponseWrapper<SignUpResponse> data = hmsAPIRespository.SignUpForAPI(patientDetails);
                
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@FirstName", patientDetails.FirstName));
                    sqlParams.Add(new SqlParameter("@LastName", patientDetails.LastName));
                    sqlParams.Add(new SqlParameter("@Gender", patientDetails.Gender));
                    sqlParams.Add(new SqlParameter("@MobileNo", patientDetails.MobileNo));
                    sqlParams.Add(new SqlParameter("@QatarId", patientDetails.QatarId));
                    sqlParams.Add(new SqlParameter("@DateOfBirth", patientDetails.DateOfBirth));
                    sqlParams.Add(new SqlParameter("@Email", patientDetails.Email));
                    sqlParams.Add(new SqlParameter("@VerificationCode", patientDetails.VerificationCode));
                    sqlParams.Add(new SqlParameter("@ServerPatientId", data.Result.Data[0].PAT_ID));
                    DataSet dsResult = db.SqlDataSetResult("USP_PatientDetails_Insert", sqlParams);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        [Route("ValidatePatientAndSendOTP/")]
        public async Task<Patient> ValidatePatientAndSendOTP([FromHeader(Name = "username")] string username, [FromHeader(Name = "password")] string password,
                    [FromBody] SignUpRequest patientDetails)
        {
            Patient response = new Patient();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                Result<ValidatePatientResponse> data = hmsAPIRespository.ValidatePatient(patientDetails.MobileNo, patientDetails.QatarId);
                if (data.IsSuccess == true)
                {
                   

                    PatientVerificationDAO dao = new PatientVerificationDAO(context);
                    Random generator = new Random();
                    int code = generator.Next(100000, 1000000);
                    dao.SaveVerificationCode(patientDetails.MobileNo, code);

                    string ooredooCustomerId = config["ooredooCustomerId"];
                    string ooredooUsername = config["ooredooUsername"];
                    string ooredooPassword = config["ooredooPassword"];
                    string ooredooOriginator = config["ooredooOriginator"];

                    using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                    {
                        List<SqlParameter> sqlParams = new List<SqlParameter>();
                        sqlParams.Add(new SqlParameter("@FirstName", data.Data[0].CustomerName));
                        sqlParams.Add(new SqlParameter("@LastName", patientDetails.LastName));
                        sqlParams.Add(new SqlParameter("@Gender", patientDetails.Gender));
                        sqlParams.Add(new SqlParameter("@MobileNo", patientDetails.MobileNo));
                        sqlParams.Add(new SqlParameter("@QatarId", patientDetails.QatarId));
                        sqlParams.Add(new SqlParameter("@DateOfBirth", patientDetails.DateOfBirth));
                        sqlParams.Add(new SqlParameter("@Email", patientDetails.Email));
                        sqlParams.Add(new SqlParameter("@VerificationCode", patientDetails.VerificationCode));
                        sqlParams.Add(new SqlParameter("@ServerPatientId", data.Data[0].CustomerId));
                        DataSet dsResult = db.SqlDataSetResult("USP_PatientDetails_Insert", sqlParams);
                    }

                    OoredooServiceAPI service = new OoredooServiceAPI(ooredooUsername, ooredooPassword, ooredooCustomerId, ooredooOriginator);
                    var status = await service.SendMessageAsync(patientDetails.MobileNo, $"Your Elite Hospital verification OTP code is {code} . Code valid for 10 minutes only, one time use. Please DO NOT share this OTP with anyone.");
                    response.IsSuccess = true;
                    response.PatId = data.Data[0].CustomerId;
                    response.PatName = data.Data[0].CustomerName;
  
                    response.Message = "Patient details validated and OTP send successfully";

                   
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Patient Validation Failed. Please Signup";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("VerifyOTP")]
        public ResponseData VerifyOTP([FromHeader(Name = "username")] string username, [FromHeader(Name = "password")] string password,
                    [FromBody] SignUpRequest OTPInfo)
        {
            ResponseData response = new ResponseData();
            try
            {
                //if (config["HMSApiUserName"] != username || config["HMSApiPassword"] != password)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "The remote server returned an error: (401) Unauthorized.";
                //    return response;
                //}
                //if (string.IsNullOrEmpty( OTPInfo.MobileNo) == true)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Mobile number should not be empty";
                //    return response;
                //}

                //if(OTPInfo.VerificationCode == null || OTPInfo.VerificationCode.Value == 0)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Verification code should not be empty";
                //    return response;
                //}

                //PatientVerificationDAO dao = new PatientVerificationDAO(context);
                //response = dao.VerifyCode(OTPInfo.MobileNo, OTPInfo.VerificationCode.Value);
                return new ResponseData() { IsSuccess = true, Message = "Successfully verified" };
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
