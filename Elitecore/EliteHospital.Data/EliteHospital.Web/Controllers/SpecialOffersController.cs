using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class SpecialOffersController : Controller
    {
        // GET: SpecialOffers
        GetAdminData adminData;
        public SpecialOffersController()
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
            ViewData["SpecialOffers"] = adminData.GetSpecialOffers();
            return View();
        }
    }
}