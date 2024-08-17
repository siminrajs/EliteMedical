using EliteHospital.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        GetAdminData adminData;
        public DoctorController()
        {
            adminData = new GetAdminData();
        }

        public ActionResult Index()
        {
            List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = adminData.GetDoctors();
            ViewData["DoctorsList"] = doctors;
            ViewData["DepartmentList"] = adminData.GetDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string Dept = form["deptmntname"];
            List <EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = adminData.GetDoctors(Dept);
            ViewData["DoctorsList"] = doctors;
            ViewData["DepartmentList"] = adminData.GetDepartments();
            ViewBag.Dept = Dept;
            return View();
        }

        
    }
}