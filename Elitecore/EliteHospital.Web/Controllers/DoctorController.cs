using EliteHospital.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EliteHospital.Data.Repository;
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
using EliteHospital.Services;
using EliteHospital.Web.ViewModel;
using EliteHospital.Web.ViewModel.Booking;
using System.Web.Security;
using EliteHospital.Data.APIRequestResponseModels.Request;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;

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
         CMSDoctorRepository repository= new CMSDoctorRepository();

        public ActionResult Index()
        {
            List<Doctor> doctorsFromServer = repository.GetAll();
            ViewData["DoctorListFromServer"] = doctorsFromServer;
            List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = adminData.GetDoctors();
            ViewData["DoctorsList"] = doctors;
            ViewData["DepartmentList"] = adminData.GetDepartments();
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //List<Doctor> doctorsFromServer = repository.GetAll();
            //ViewData["DoctorListFromServer"] = doctorsFromServer;
            string Dept = form["deptmntname"];

            List<Doctor> doctorslist = repository.GetDeptName(Dept);
            ViewData["DoctorListFromServer"] = doctorslist;
            //List <EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = adminData.GetDoctors(Dept);
            ViewData["DoctorsList"] = doctorslist;
            ViewData["DepartmentList"] = adminData.GetDepartments();
            ViewBag.Dept = Dept;
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View();
        }
       
        public ActionResult ViewDoctorBio(string Doctorid,string Doctorname,string Departmet,string speciality=null)
        {
            //ViewBag.Description1 = Description1;
            Doctor doctordetails = repository.GetDoctorDetails(Doctorid);
            if(doctordetails!=null)
            {
                ViewBag.DoctorImage = doctordetails.Doctor_Image;
                ViewBag.Description1 = doctordetails.Description1;
                ViewBag.Description2 = doctordetails.Description2;
                ViewBag.Description3 = doctordetails.Description3;
                ViewBag.DoctorBioStatus = doctordetails.DoctorBioStatus;
            }
            ViewBag.Doctorname = Doctorname;
            ViewBag.Departmet = Departmet;
            ViewBag.speciality = speciality;
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