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
    public class MediaController : Controller
    {
        CMSMediaRepository repository = new CMSMediaRepository();
        // GET: Media
        GetAdminData adminData;
        public MediaController()
        {
            adminData = new GetAdminData();
        }
        public ActionResult Index(int Id=0)
        {          
            List<tbl_Media> banners = repository.GetAll();
            var duplicatesRemoved = banners.Distinct().ToList();
            ViewData["CovidBannerList"] = banners;
            List<tbl_MediaImagesList> ImageList = repository.GetImagelist();
            ViewData["ImageList"] = ImageList;

            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View();
        }
  
        public ActionResult MediaPartialView(int Id)
        {
            List<tbl_Media> banner = repository.Getlist(Id);
            List<tbl_MediaImagesList> ImageList = repository.GetImage(banner[0].Id);
            ViewData["ImageList"] = ImageList;
            return View();
        }

    }
}