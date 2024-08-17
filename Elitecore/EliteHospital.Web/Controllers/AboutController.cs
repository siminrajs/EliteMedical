using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        GetAdminData adminData;
        public AboutController()
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
            ViewBag.AboutUs = adminData.GetAboutUs().FirstOrDefault();
            return View();
        }
    }
}