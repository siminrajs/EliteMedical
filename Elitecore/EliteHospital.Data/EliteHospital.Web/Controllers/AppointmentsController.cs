using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.Repository;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    [AreaAuthorize("Site")]
    public class AppointmentsController : Controller
    {
        // GET: Appointments

        HMSAPIRespository hmsAPIRespository;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AppointmentsController()
        {
            hmsAPIRespository = new HMSAPIRespository();
        }

        public ActionResult Index()
        {
            //int PatId = Convert.ToInt32(Session["QatarId"]);
            int PatId = Convert.ToInt32(Session["PatientID"]);
            logger.Info("Loading Appointments list. Patient Id-"+ PatId);
            List <AppointmentDtlsResponse> appointments = new List<AppointmentDtlsResponse>();
            appointments = hmsAPIRespository.GetAppointmentsOfPatient(PatId);
            ViewData["Appointments"] = appointments;
            logger.Info("Appointment list loaded for the patient and data returning to view");
            return View();
        }

        public ActionResult AppointmentDetails(int Id)
        {
            try
            {
                logger.Info("Fetching appointment details for the appointment id -" + Id);
                if(Id <= 0)
                {
                    throw new Exception("Appointment Id should not be 0.");
                }
                AppointmentDtlsResponse appointmentdtls = new AppointmentDtlsResponse();
                if (appointmentdtls != null)
                {
                    logger.Info("Appointment details fetched successfully");
                    appointmentdtls = hmsAPIRespository.GetAppointmentDtls(Id);
                }  
                else
                {
                    logger.Info("Not able to fetch Appointment details");
                }
                return View(appointmentdtls);
            }
            catch (Exception ex)
            {
                logger.Error("Exception in AppointmentDetails action in Appointment controller");
                logger.Error(ex);
                return View(new AppointmentDtlsResponse());
            }
        }

        public ActionResult CancelAppointment(int Id)
        {
            try
            {
                logger.Info("Cancelling started for the appointment id - " + Id);
                string response = hmsAPIRespository.CancelAppointment(Id);
                ViewData["Response"] = response;
                ViewBag.AppointmentDtls = hmsAPIRespository.GetAppointmentDtls(Id);
                logger.Info("Cancelling completed");
                return View();
            }
            catch (Exception ex)
            {
                logger.Info("Error while cancelling the appointment for the appointment id -" + Id);
                logger.Error(ex);
                return View(ex.Message);
            }
        }
    }
}