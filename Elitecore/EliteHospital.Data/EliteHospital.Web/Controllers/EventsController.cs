using EliteHospital.Core;
using EliteHospital.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        GetAdminData adminData;
        public EventsController()
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

        public ActionResult EventList()
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