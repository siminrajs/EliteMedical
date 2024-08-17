using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media
        GetAdminData adminData;
        public MediaController()
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