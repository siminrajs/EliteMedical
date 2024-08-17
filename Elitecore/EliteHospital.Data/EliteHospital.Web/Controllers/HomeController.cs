using EliteHospital.Core;
using EliteHospital.Data;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Models;
using EliteHospital.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class HomeController : Controller
    {
        GetAdminData adminData;
        CMSBannerRepository bannerRepository;
        public HomeController()
        {
            adminData  = new GetAdminData();
            bannerRepository = new CMSBannerRepository();
        }
        public ActionResult Index()
        {
            List < EliteHospital.Data.APIRequestResponseModels.Response.Doctor > doctors = adminData.GetDoctors();
            ViewData["DoctorsList"] = doctors;
            ViewData["BannerList"] = GetBanners();
            ViewData["DepartmentList"] = adminData.GetDepartments();
            ViewData["Insurances"] = adminData.GetInsurances();
            ViewBag.ShortDescription = adminData.GetAboutUs().FirstOrDefault().ShortDescription;
            ViewBag.DoctorFirst = doctors.FirstOrDefault();
            ContactU contact = adminData.GetContactUs();
            if(contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return this.View();
        }

        public List<Banner> GetBanners()
        {
            try
            {
                return bannerRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}