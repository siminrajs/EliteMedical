using EliteHospital.Data.Repository;
using EliteHospital.Services;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Security;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data;
using System.Globalization;
using EliteHospital.Data.APIRequestResponseModels.Request;

namespace EliteHospital.Web.Controllers
{
    [AreaAuthorize("Site")]
    public class BookingController : Controller
    {
        HMSApiService hmsApiService;
        HMSAPIRespository hmsAPIRespository;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BookingController()
        {
            hmsApiService = new HMSApiService();
            hmsAPIRespository = new HMSAPIRespository();
        }
        CMSDoctorRepository repository = new CMSDoctorRepository();
        CMSDepartmentRepository deptrepository = new CMSDepartmentRepository();

        // GET: Booking
        public ActionResult Index()
        {
           
            List<EliteHospital.Core.Doctor> doctorsFromServer = repository.GetAll();
            ViewData["DoctorListFromServer"] = doctorsFromServer;
            logger.Info("Loading Book Appointment page");
            //ViewData["Departments"] = GetDepartments();
            List<EliteHospital.Core.Department> departments = deptrepository.GetAllBooking();
            ViewData["Departments"] = departments;

            ViewData["Doctors"] = GetDoctors();
            logger.Info("Loading Book Appointment page is completed");
            return View();
        }

        public ActionResult ShowAppointmentSuccess(int AppointmentId)
        {
            logger.Info("Inside ShowAppointmentSuccess action in Booking controller. Input AppointmentId-" + AppointmentId);
            AppointmentDtlsResponse appointmentDtls = hmsAPIRespository.GetAppointmentDtls(AppointmentId);
            if (appointmentDtls == null)
            {
                logger.Info("Unable to fetch Appointment details");
                appointmentDtls = new AppointmentDtlsResponse();
            }
            else
            {
                logger.Info("Appointment details are fetched successfully");
            }
            return PartialView("BookAppointmentSuccess", appointmentDtls);
        }

        public ActionResult SaveAppointment(SaveAppointment appointmentInfo)
        {
            try
            {
                logger.Info("Save appointment is started.");
                logger.Info(appointmentInfo);
                //appointmentInfo.PatId = Convert.ToInt32(Session["QatarId"]);
                appointmentInfo.PatId = Convert.ToInt32(Session["PatientID"]);
                SaveAppointmentResponse response = hmsAPIRespository.SaveAppointment(appointmentInfo);
                if (response != null)
                {
                    logger.Info("Booking saved successfully");
                    return Json(new { success = true, AppointmentId = response.AppointmentId, Message = "Booking saved successfully." });
                }
                else
                {
                    logger.Info("Booking failed. Please try again later.");
                    return Json(new { success = false, Message = "Booking failed. Please try again later." });
                }
            }
            catch (Exception ex)
            {
                logger.Info("Error while saving appointment details");
                logger.Error(ex);
                return Json(new { success = false, Message = ex.Message });
            }
        }

        public ActionResult BookYourConsultation(string doctorId, string date = null)
        {
           
            try
            {
                logger.Info("Fetching Book Consultation view for the doctorId -" + doctorId + " and date-" + date);
                Doctor doctor = hmsAPIRespository.GetDoctor(doctorId);
                EliteHospital.Core.Doctor doctordb = repository.GetDoctorDetails(doctorId);
                ViewBag.Doctor = doctordb;
                ViewBag.Image = doctordb.Doctor_Image;
                ViewBag.DepartmentName = doctordb.DepartmentName;
                AssignSlots(doctorId, date);
                logger.Info("Fetching Book Consultation view is completed");
                return View();
            }
            catch (Exception ex)
            {
                logger.Info("Error while fetching the Book consulation view");
                logger.Error(ex);
                return View("Error while fetching the Book consulation view");
            }
        }

        public void AssignSlots(string doctorId, string date)
        {
            try
            {
                logger.Info("Fetching slots for the doctorId -" + doctorId + " and date-" + date);
                DateTime fromdate;
                DateTime todate;
                if (date == null)
                { fromdate = todate = DateTime.Now.AddDays(1); }
                else
                { fromdate = todate = DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture); }

                List<DoctorSlot> slots = GetDoctorSlots(doctorId, fromdate, todate);
                if (slots.Count > 0)
                { ViewData["Slots"] = slots; }
                else
                { ViewData["Slots"] = null; }
                logger.Info("Fetching slots completed");
            }
            catch (Exception ex)
            {
                logger.Info("Error while Fetching slots");
                logger.Error(ex);
                throw ex;
            }
        }

        public ActionResult GetPatientDtls(string DoctorId)
        {
            try
            {
                logger.Info("Fetching patient details view");
                ViewBag.DoctorId = DoctorId;
                return PartialView("PatientDetails");
            }
            catch (Exception ex)
            {
                logger.Info("Fetching patient details view failed");
                logger.Error(ex);
                return Content("");
            }
        }

        public ActionResult GetSlotsView(string doctorId, string date)
        {
            AssignSlots(doctorId, date);
            return PartialView("Slots");
        }

        public ActionResult PatientDetails()
        {
            return View();
        }
        public ActionResult MyProfile()
        {
            logger.Info("Inside MyProfile action");
            int patientId = Convert.ToInt32(HttpContext.User.Identity.Name);
            logger.Info("Patient Id -" + Convert.ToString(patientId));
            Result<Profile> profile = hmsApiService.GetPatientDetails(patientId);
            Profile profileinfo = new Profile();
            if (profile.IsSuccess == true && profile.Data != null)
            {
                profileinfo = profile.Data[0];
            }
            return View(profileinfo);
        }

        public List<Department> GetDepartments()
        {
            try
            {
                CMSDepartmentRepository departmentRepository = new CMSDepartmentRepository();
                logger.Info("Fetching departments list");
                List<Department> departmentsFromAPI = hmsAPIRespository.GetAllDepartments();
                List<Core.Department> departmentImages = departmentRepository.GetAll();
                foreach (Core.Department item in departmentImages)
                {
                    Department departmentToUpdate = departmentsFromAPI.Where(p => p.DepartmentName.Trim().ToUpper() == item.DepartmentName.Trim().ToUpper()).FirstOrDefault();
                    if (departmentToUpdate != null)
                    {
                        departmentToUpdate.Status = item.Status;
                    }
                }
                List<Department> filteredlist = departmentsFromAPI.Where(p => p.Status == "Y" || p.Status == null).ToList();
                return filteredlist;
            }
            catch (Exception ex)
            {
                logger.Info("Fetching departments failed");
                logger.Error(ex);
                throw ex;
            }
        }

        public List<DoctorSlot> GetDoctorSlots(string doctorId, DateTime fromdate, DateTime todate)
        {
            try
            {
                logger.Info("Inside GetDoctorSlots function");
                List<DoctorSlot> slots = new List<DoctorSlot>();
                Result<DoctorSlot> data = hmsAPIRespository.GetDoctorSlots(doctorId, fromdate, todate);
                if (data != null && data.Data != null)
                {
                    foreach (DoctorSlot item in data.Data)
                    {
                        slots.Add(new DoctorSlot()
                        {
                            AppointmentDate = item.AppointmentDate,
                            StartTime = item.StartTime,
                            EndTime = item.StartTime,
                            Interval = item.Interval
                        });
                    }
                }
                logger.Info("Fetching slots completed");
                return slots;
            }
            catch (Exception ex)
            {
                logger.Info("Error while fetching slots for the doctorId-" + doctorId + ", date-" + fromdate);
                logger.Error(ex);
                throw ex;
            }
        }

        public List<Doctor> GetDoctors()
        {
            try
            {
                CMSDoctorRepository doctorRepository = new CMSDoctorRepository();
                logger.Info("Fetching doctors list");
                List<Doctor> doctorsFromAPI = hmsAPIRespository.GetAllDoctors();
                List<Core.Doctor> doctorImages = doctorRepository.GetAll();
                foreach (Core.Doctor item in doctorImages)
                {
                    Doctor doctorToUpdate = doctorsFromAPI.Where(p => p.DoctorId.Trim().ToUpper() == item.DoctorId.Trim().ToUpper()).FirstOrDefault();
                    if (doctorToUpdate != null)
                    {
                        doctorToUpdate.Photo = item.DoctorImage;
                        doctorToUpdate.Status = item.Status;
                        doctorToUpdate.DoctorBioStatus = item.DoctorBioStatus;
                    }
                }
                List<Doctor> filteredlist = doctorsFromAPI.Where(p => p.Status == "Y" || p.Status == null).ToList();
                return filteredlist;
            }
            catch (Exception ex)
            {
                logger.Info("error while Fetching doctors list");
                logger.Error(ex);
                throw ex;
            }
        }

        public ActionResult LogOut()
        {
            logger.Info("Inside logout function");
            FormsAuthentication.SignOut();
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            Session.Abandon();
            logger.Info("Log out completed and returning to log in view");
            return RedirectToAction("Index", "Account");
        }
    }
}