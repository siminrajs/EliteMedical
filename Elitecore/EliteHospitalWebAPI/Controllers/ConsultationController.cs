using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.HMSAPIHelpers;
using EliteHospital.Data.Repository;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        HMSAPIRespository hmsAPIRespository;
        private readonly IConfiguration _config;
        string BaseUrl = string.Empty;
        private readonly EliteHospitalContext context;

        public ConsultationController(IConfiguration configuration, EliteHospitalContext _context)
        {
            _config = configuration;
            BaseUrl = _config["HMSApiBaseUrl"];
            context = _context;
            //string username = _config["HMSApiUserName"];
            //string password = _config["HMSApiPassword"];

        }

        // GET: api/<ConsultationController>
        [HttpGet]
        [Route("TestMethod/")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("GetAllDoctors/")]
        public Result<DoctorModel> GetAllDoctors([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            Result<DoctorModel> doctors = new Result<DoctorModel>();
           
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                ResponseWrapper<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctorsFromAPI = hmsAPIRespository.GetAllDoctorsForAPI();
                if (string.IsNullOrEmpty(inputs.DepartmentId))
                {
                    doctors = UpdateDoctorImages(doctors, doctorsFromAPI.Result.Data);                                     
                }
                else
                {
                    List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> filteredDoctorlist = doctorsFromAPI.Result.Data.Where(p => p.DepartmentId == inputs.DepartmentId).ToList();
                    doctors = UpdateDoctorImages(doctors, filteredDoctorlist);
                }
                return doctors;
            }
            catch (Exception ex)
            {
                doctors.IsSuccess = false;
                doctors.Message = ex.Message;
                return doctors;
            }
        }

        private Result<DoctorModel> UpdateDoctorImages(Result<DoctorModel> doctors, List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctorsFromAPI)
        {
            List<Doctor> doctorImages = context.Doctors.ToList();
            List<Department> departments = context.Departments.ToList();
            foreach (Doctor item in doctorImages)
            {
                doctors.Data = doctorsFromAPI.Select(p => new DoctorModel()
                {
                    DoctorId = p.DoctorId,
                    DoctorName = p.DoctorName,
                    DepartmentId = p.DepartmentId,
                    DepartmentName = p.DepartmentName,
                    DepartmentNameArabic = departments.Where(j => j.DepartmentName == p.DepartmentName).FirstOrDefault() != null ? departments.Where(j => j.DepartmentName == p.DepartmentName).FirstOrDefault().DepartmentNameArabic : "",
                    Designation = p.Designation,
                    DoctorBioStatus = item.DoctorBioStatus,
                    Status = p.Status
                }).ToList();
            }

            foreach (Doctor item in doctorImages)
            {
                DoctorModel doctorToUpdate = doctors.Data.Where(p => p.DoctorId.Trim().ToUpper() == item.DoctorId.Trim().ToUpper()).FirstOrDefault();
                if (doctorToUpdate != null)
                {
                    doctorToUpdate.DoctorImage = item.DoctorImage;
                    doctorToUpdate.DoctorImageMob = item.DoctorImageMob;
                    doctorToUpdate.Status = item.Status;
                    doctorToUpdate.DoctorNameArabic = item.DoctorNameArabic;
                    doctorToUpdate.DoctorBioStatus = item.DoctorBioStatus;                   
                }
            }
            doctors.Data = doctors.Data.Where(p => p.Status == "Y" || p.Status == null).ToList();
            return doctors;
        }


        [HttpGet]
        [Route("GetBookingStatus/")]
        public DoctorBooking<DoctorStatus> GetBookingStatus(string doctorId)
        {
            DoctorBooking<DoctorStatus> result = new DoctorBooking<DoctorStatus>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@DoctorId", doctorId));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetBookingStatus", sqlParams);
                    string DoctorId = "";
                    string BookingStatus = "";
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        DoctorId = dsResult.Tables[0].Rows[0]["DoctorId"].ToString();
                        BookingStatus = dsResult.Tables[0].Rows[0]["BookingStatus"].ToString();
                        result.DoctorId = DoctorId;
                        result.BookingStatus = BookingStatus;
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public class DoctorBooking<T> : DoctorStatus
        {

            public string DoctorId { get; set; }
            public string BookingStatus { get; set; }
        }

        public class DoctorStatus
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }
        [HttpPost]
        [Route("GetAllDepartments/")]
        public Result<DepartmentModel> GetAllDepartments([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password)
        {
            Result<DepartmentModel> departments = new Result<DepartmentModel>();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                ResponseWrapper<EliteHospital.Data.APIRequestResponseModels.Response.Department> departmentsfromAPI = hmsAPIRespository.GetAllDepartmentsForAPI();

                List<Department> departmentImages = context.Departments.ToList();
                departments.Data = departmentsfromAPI.Result.Data.Select(p => new DepartmentModel()
                {
                    DepartmentId = p.DepartmentId,
                    DepartmentName = p.DepartmentName
                }).ToList();

                foreach (Department item in departmentImages)
                {
                    DepartmentModel departmentToUpdate = departments.Data.Where(p => p.DepartmentName.Trim().ToUpper() == item.DepartmentName.Trim().ToUpper()).FirstOrDefault();
                    if (departmentToUpdate != null)
                    {
                        departmentToUpdate.DepartmentNameArabic = item.DepartmentNameArabic;
                        departmentToUpdate.LongDescription = item.LongDescription;
                        departmentToUpdate.Description = item.Description;
                        departmentToUpdate.DescriptionArabic = item.DescriptionArabic;
                        departmentToUpdate.DepartmentImage = item.DepartmentImage;
                        departmentToUpdate.DepartmentImageMob = item.DepartmentImageMob;
                        departmentToUpdate.Status = item.Status;
                        departmentToUpdate.OrderNo = item.OrderNo;
                    }
                }
                departments.Data = departments.Data.Where(p => p.Status == "Y" || p.Status == null).ToList();
                departments.Data = departments.Data.OrderBy(p => p.OrderNo == null)
                       .ThenBy(p => p.OrderNo).ToList();
                departments.IsSuccess = true;
                departments.Message = "Departments fetched successfully";
                return departments;
            }
            catch (Exception ex)
            {
                departments.IsSuccess = false;
                departments.Message = ex.Message;
                return departments;
            }
        }

    }
}
