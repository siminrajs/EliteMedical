using EliteHospital.Data.APIRequestResponseModels.Request;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.HMSAPIHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class HMSAPIRespository
    {
        readonly APIHelper apiHelper;
        public HMSAPIRespository(bool IsFromWebAPI = false, string baseurl = "", string username="", string password="")
        {
            if(IsFromWebAPI == true)
            {
                apiHelper = new APIHelper();
                apiHelper.IsFromWebAPI = true;
                apiHelper.BaseUrl = baseurl;
                apiHelper.UserName = username;
                apiHelper.Password = password;
            }
            else
            {
                apiHelper = new APIHelper();
            }            
        }

        public Result<DoctorSlot> GetDoctorSlots(string DoctorId, DateTime FromDate, DateTime ToDate)
        {
            var requestBody = new { DoctorId = DoctorId, FromDt = FromDate, ToDt = ToDate };
            var requestBodyJson = JsonConvert.SerializeObject(requestBody);
            var data = apiHelper.ExecuteRequest<ResponseWrapper<DoctorSlot>>(APIConstants.GetDoctorSlots, requestBodyJson);
            return data.Result;
        }

        public ResponseWrapper<DoctorSlot> GetDoctorSlotsForAPI(string DoctorId, DateTime FromDate, DateTime ToDate)
        {
            var requestBody = new { DoctorId = DoctorId, FromDt = FromDate, ToDt = ToDate };
            var requestBodyJson = JsonConvert.SerializeObject(requestBody);
            return apiHelper.ExecuteRequest<ResponseWrapper<DoctorSlot>>(APIConstants.GetDoctorSlots, requestBodyJson);
        }


        public List<AppointmentDtlsResponse> GetAppointmentsOfPatient(int patientId)
        {
            try
            {
                var requestBody = new { PatId = patientId };
                var requestBodyJson = JsonConvert.SerializeObject(requestBody);
                var data = apiHelper.ExecuteRequest<ResponseWrapper<AppointmentDtlsResponse>>(APIConstants.GetAllUpcomingAppointments, requestBodyJson);
                return data.Result.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseWrapper<AppointmentDtlsResponse> GetAppointmentsOfPatientForAPI(int patientId)
        {
            try
            {
                var requestBody = new { PatId = patientId };
                var requestBodyJson = JsonConvert.SerializeObject(requestBody);
                return apiHelper.ExecuteRequest<ResponseWrapper<AppointmentDtlsResponse>>(APIConstants.GetAllUpcomingAppointments, requestBodyJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AppointmentDtlsResponse GetAppointmentDtls(int appointmentId)
        {
            try
            {
                var requestBody = new { AppointmentId = appointmentId };
                var requestBodyJson = JsonConvert.SerializeObject(requestBody);
                var data = apiHelper.ExecuteRequest<ResponseWrapper<AppointmentDtlsResponse>>(APIConstants.GetAppointmentDetails, requestBodyJson);
                AppointmentDtlsResponse appointment = null;
                if (data != null && data.Result != null && data.Result.Data != null && data.Result.Data.Count > 0)
                {
                    appointment = data.Result.Data.First();
                }
                return appointment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseWrapper<AppointmentDtlsResponse> GetAppointmentDtlsForAPI(int appointmentId)
        {
            try
            {
                var requestBody = new { AppointmentId = appointmentId };
                var requestBodyJson = JsonConvert.SerializeObject(requestBody);
                return apiHelper.ExecuteRequest<ResponseWrapper<AppointmentDtlsResponse>>(APIConstants.GetAppointmentDetails, requestBodyJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SaveAppointmentResponse SaveAppointment(SaveAppointment appointmentInfo)
        {
            DateTime consultation = DateTime.ParseExact(appointmentInfo.Date + " " + appointmentInfo.Time,
                "MMM dd yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            appointmentInfo.ConsultationOn = consultation.ToString("yyyy-MM-dd HH:mm:ss.ffff");

            var requestBodyJson = JsonConvert.SerializeObject(appointmentInfo);
            var data = apiHelper.ExecuteRequest<ResponseWrapper<SaveAppointmentResponse>>(APIConstants.SaveAppointmentDetails, requestBodyJson);
            SaveAppointmentResponse saveAppointmentResponse = null;
            if (data != null && data.Result != null && data.Result.IsSuccess == true &&
                data.Result.Data != null && data.Result.Data.Count > 0)
            {
                saveAppointmentResponse = data.Result.Data[0];
            }
            else if (data != null && data.Result != null && data.Result.IsSuccess == false)
            {
                throw new Exception(data.Result.Message);
            }
            return saveAppointmentResponse;
        }

        public string CancelAppointment(int appointmentId)
        {
            var requestBodyJson = JsonConvert.SerializeObject(new { AppointmentId = appointmentId });
            var data = apiHelper.ExecuteRequest<ResponseWrapper<string>>(APIConstants.CancelAppointment, requestBodyJson);
            string responsemsg = "";
            if (data != null && data.Result != null)
            {
                responsemsg = data.Result.Message;
            }
            return responsemsg;
        }

        public ResponseWrapper<string> CancelAppointmentForAPI(int appointmentId)
        {
            var requestBodyJson = JsonConvert.SerializeObject(new { AppointmentId = appointmentId });
            return apiHelper.ExecuteRequest<ResponseWrapper<string>>(APIConstants.CancelAppointment, requestBodyJson);
        }

        public List<Department> GetAllDepartments()
        {
            var data = apiHelper.ExecuteRequest<ResponseWrapper<Department>>(APIConstants.GetAllDepartments);
            List<Department> departments = new List<Department>();
            if (data != null)
            {
                foreach (Department item in data.Result.Data)
                {
                    departments.Add(new Department()
                    {
                        DepartmentId = item.DepartmentId,
                        DepartmentName = item.DepartmentName
                    });
                }
            }
            return departments;
        }

        public ResponseWrapper<Department> GetAllDepartmentsForAPI()
        {
            return apiHelper.ExecuteRequest<ResponseWrapper<Department>>(APIConstants.GetAllDepartments);
        }

        public ResponseWrapper<Doctor> GetAllDoctorsForAPI()
        {
            return apiHelper.ExecuteRequest<ResponseWrapper<Doctor>>(APIConstants.GetAllDoctors);
        }

        public List<Doctor> GetAllDoctors()
        {
            var data = apiHelper.ExecuteRequest<ResponseWrapper<Doctor>>(APIConstants.GetAllDoctors);
            List<Doctor> doctors = new List<Doctor>();
            if (data != null)
            {
                foreach (Doctor item in data.Result.Data)
                {
                    doctors.Add(new Doctor()
                    {
                        DoctorId = item.DoctorId,
                        DoctorName = item.DoctorName,
                        Designation = item.Designation,
                        DepartmentId = item.DepartmentId,
                        DepartmentName = item.DepartmentName,
                        Speciaity = item.Speciaity
                    });
                }
            }
            return doctors;
        }

        public Doctor GetDoctor(string Id)
        {
            var data = apiHelper.ExecuteRequest<ResponseWrapper<Doctor>>(APIConstants.GetAllDoctors);
            Doctor doctor = null;
            if (String.IsNullOrEmpty(Id) == false)
            {
                doctor = data.Result.Data.Where(p => p.DoctorId.ToUpper() == Id.ToUpper()).FirstOrDefault();
            }
            return doctor;
        }

        public Result<ValidatePatientResponse> ValidatePatient(string mobileNo, string qatarId)
        {
            var requestBody = new ValidatePatientRequest() { MobileNo = mobileNo, QatarId = qatarId };
            var requestBodyJson = JsonConvert.SerializeObject(requestBody);
            var data = apiHelper.ExecuteRequest<ResponseWrapper<ValidatePatientResponse>>(APIConstants.ValidatePatient, requestBodyJson);
            if(data.Result.IsSuccess == false)
            {
                mobileNo = mobileNo.Replace("+91", "");
                mobileNo = mobileNo.Replace("+974", "");
                var _requestBody = new ValidatePatientRequest() { MobileNo = mobileNo, QatarId = qatarId };
                var _requestBodyJson = JsonConvert.SerializeObject(_requestBody);
                var _data = apiHelper.ExecuteRequest<ResponseWrapper<ValidatePatientResponse>>(APIConstants.ValidatePatient, _requestBodyJson);
                return _data.Result;
            }
            else
            {
                return data.Result;
            }
        }

        public Result<SignUpResponse> SignUp(SignUpRequest signUpRequest)
        {
            var requestBodyJson = JsonConvert.SerializeObject(signUpRequest);
            var data = apiHelper.ExecuteRequest<ResponseWrapper<SignUpResponse>>(APIConstants.SavePatientDetails, requestBodyJson);
            return data.Result;
        }

        public ResponseWrapper<SignUpResponse> SignUpForAPI(SignUpRequest signUpRequest)
        {
            var requestBodyJson = JsonConvert.SerializeObject(signUpRequest);
            return apiHelper.ExecuteRequest<ResponseWrapper<SignUpResponse>>(APIConstants.SavePatientDetails, requestBodyJson);
        }

        public Result<Profile> GetPatientDetails(int patientid)
        {
            var requestData = new { PatId = patientid };
            var requestBodyJson = JsonConvert.SerializeObject(requestData);
            var data = apiHelper.ExecuteRequest<ResponseWrapper<Profile>>(APIConstants.GetPatientDetails, requestBodyJson);
            return data.Result;
        }

        public ResponseWrapper<Profile> GetPatientDetailsForAPI(int patientid)
        {
            var requestData = new { PatId = patientid };
            var requestBodyJson = JsonConvert.SerializeObject(requestData);
            return apiHelper.ExecuteRequest<ResponseWrapper<Profile>>(APIConstants.GetPatientDetails, requestBodyJson);
        }
    }
}
