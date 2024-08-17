using EliteHospital.Core;
using EliteHospital.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class OnlineConsultationController : Controller
    {
        // GET: OnlineConsultation
        GetAdminData adminData;
        public OnlineConsultationController()
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
            OnlineConsultation onlineConsultation = adminData.GetOnlineConsultation();
            ViewData["VideoUrls"] = onlineConsultation.VideoUrls.ToList();
            return View(onlineConsultation);
        }
    }
}