using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Request;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.Repository;
using EliteHospital.Services;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailsController : ControllerBase
    {
        HMSAPIRespository hmsAPIRespository;
        private readonly IConfiguration config;
        private readonly EliteHospitalContext context;
        string BaseUrl = string.Empty;
        public AppointmentDetailsController(IConfiguration configuration, EliteHospitalContext _context)
        {
            config = configuration;
            context = _context;
            BaseUrl = config["HMSApiBaseUrl"];
        }

        [HttpPost]
        [Route("GetAppointmentFreeSlotsByConsId/")]
        public ResponseWrapper<DoctorSlot> GetAppointmentFreeSlotsByConsId([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            ResponseWrapper<DoctorSlot> result = new ResponseWrapper<DoctorSlot>();
            result.Result = new Result<DoctorSlot>();
            try
            {
                DateTime slotdate;
                if (string.IsNullOrEmpty(inputs.SlotDate))
                {
                    result.Result.IsSuccess = false;
                    result.Result.Message = "Slot Date should not be empty";
                }

                if (DateTime.TryParse(inputs.SlotDate, out slotdate))
                {
                    slotdate = DateTime.ParseExact(inputs.SlotDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else
                {
                    result.Result.IsSuccess = false;
                    result.Result.Message = "Slot Date should not be in YYYY-MM-DD format";
                }

                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                var data = hmsAPIRespository.GetDoctorSlotsForAPI(inputs.DoctorId, slotdate, slotdate);
                return data;
            }
            catch (Exception ex)
            {
                result.Result.IsSuccess = false;
                result.Result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("GetAllUpcomingAppointmentsByPatientId/")]
        public ResponseWrapper<AppointmentDtlsResponse> GetAllUpcomingAppointmentsByPatientId([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            ResponseWrapper<AppointmentDtlsResponse> result = new ResponseWrapper<AppointmentDtlsResponse>();
            result.Result = new Result<AppointmentDtlsResponse>();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                var doctors = hmsAPIRespository.GetAppointmentsOfPatientForAPI(inputs.PatId);

                if(doctors.Result.Data == null)
                {
                    result.Result.IsSuccess = true;
                    result.Result.Message = "No record(s) found";
                    return result;
                }
                doctors.Result.Data = doctors.Result.Data.Select(x => { x.DoctorPhoto = null; return x; }).ToList();
                List<Doctor> doctorImages = context.Doctors.ToList();
                foreach (Doctor item in doctorImages)
                {
                    AppointmentDtlsResponse doctorToUpdate = doctors.Result.Data.Where(p => p.DoctorName.Trim().ToUpper() == item.DoctorName.Trim().ToUpper()).FirstOrDefault();
                    if (doctorToUpdate != null)
                    {
                        doctorToUpdate.DoctorPhoto = item.DoctorImageMob;
                        doctorToUpdate.DoctorNameArabic = item.DoctorNameArabic;
                        doctorToUpdate.DepartmentArabic = context.Departments.Where(p => p.DepartmentName == doctorToUpdate.Designation).Count() > 0 ? context.Departments.Where(p => p.DepartmentName == doctorToUpdate.Designation).FirstOrDefault().DepartmentNameArabic : "";
                        
                    }
                }
                return doctors;
            }
            catch (Exception ex)
            {
                result.Result.IsSuccess = false;
                result.Result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("CancelAppointmentByAppId/")]
        public ResponseWrapper<string> CancelAppointmentByAppId([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            ResponseWrapper<string> result = new ResponseWrapper<string>();
            result.Result = new Result<string>();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                var data = hmsAPIRespository.CancelAppointmentForAPI(inputs.AppointmentId);
                return data;
            }
            catch (Exception ex)
            {
                result.Result.IsSuccess = false;
                result.Result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("GetAppointmentDetailsByAppId/")]
        public ResponseWrapper<AppointmentDtlsResponse> GetAppointmentDetailsByAppId([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            ResponseWrapper<AppointmentDtlsResponse> result = new ResponseWrapper<AppointmentDtlsResponse>();
            result.Result = new Result<AppointmentDtlsResponse>();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                var data = hmsAPIRespository.GetAppointmentDtlsForAPI(inputs.AppointmentId);
                return data;
            }
            catch (Exception ex)
            {
                result.Result.IsSuccess = false;
                result.Result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("SaveAppointmentDetails/")]
        public Result<SaveAppointmentResponse> SaveAppointmentDetails([FromHeader(Name = "username")] string username,
            [FromHeader(Name = "password")] string password,
            [FromBody] APIInputs inputs)
        {
            Result<SaveAppointmentResponse> result = new Result<SaveAppointmentResponse>();
            try
            {
                hmsAPIRespository = new HMSAPIRespository(true, BaseUrl, username, password);
                SaveAppointment saveAppointment = new SaveAppointment()
                {
                    DoctorId = inputs.DoctorId,
                    PatId = inputs.PatId,
                    Date = inputs.Date,
                    Time = inputs.Time,
                    IsBookForAnotherPerson = inputs.IsBookForAnotherPerson,
                    BookingForName = inputs.BookingForName,
                    BookingForGender = inputs.BookingForGender,
                    BookingForRelation = inputs.BookingForRelation
                };             

                SaveAppointmentResponse data = hmsAPIRespository.SaveAppointment(saveAppointment);
                List<SaveAppointmentResponse> responsedata = new List<SaveAppointmentResponse>();
                responsedata.Add(data);

                result.IsSuccess = true;
                result.Data = responsedata;
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
