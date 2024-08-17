using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class SurgicalProceduresController : Controller
    {
        // GET: SurgicalProcedures
        GetAdminData adminData;
        public SurgicalProceduresController()
        {
            adminData = new GetAdminData();
        }
        public ActionResult Index()
        {
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View();
        }
    }
}