using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Web.Mvc;
using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        GetAdminData adminData;
        CMSEventRepository repository = new CMSEventRepository();
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
            var type = "Events";
            List<Event> Eventlist = repository.GetAll(type);
            ViewData["EventList"] = Eventlist;
            return View();
        }
        public ActionResult IndexBlog()
        {
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            var type = "Blog";
            List<Event> Eventlist = repository.GetAll(type);
            ViewData["EventList"] = Eventlist;
            return View();
        }
        public ActionResult EventList(int Id)
        {
            var type = "Events";
            List<Event> Image = repository.Getdetails(Id,type);
             ViewData["ImageList"] = Image;
            List<EventImagesList> EventImagesList = repository.Getimagedetails(Id);
            ViewData["EventImagesList"] = EventImagesList;
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View();
        }

        public ActionResult EventListBlog(int Id)
        {
            var type = "Blog";
            List<Event> Image = repository.Getdetails(Id,type);
            ViewData["ImageList"] = Image;
            List<EventImagesList> EventImagesList = repository.Getimagedetails(Id);
            ViewData["EventImagesList"] = EventImagesList;
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