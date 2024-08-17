using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace EliteHospital.Web.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        GetAdminData adminData;
        public DepartmentController()
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
            ViewData["Departments"] = adminData.GetDepartments();
            return View();
        }

        public ActionResult Departmentdtls(string dept)
        {
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            if (dept == "DERMATOLOGY")
            {
                return RedirectToAction("DermatologySkincare", new {  });
            }
            else if (dept == "PLASTIC SURGERY")
            {
                return RedirectToAction("PlasticSurgery", new { });
            }
            else
            {
                List<Department> departments = adminData.GetDepartments();
                Department department = new Department();
                department = departments.Where(p => p.DepartmentName.ToUpper() == dept.ToUpper()).FirstOrDefault();
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> deptDoctors = adminData.GetDoctors(dept);
                ViewData["DeptDoctors"] = deptDoctors;
                return View(department);
            }
        }

        public ActionResult DermatologySkincare()
        {
            return View();
        }
        
        public ActionResult SurgicalProcedures()
        {
            return View();
        }

        public ActionResult PlasticSurgery()
        {
            return View();
        }

        public ActionResult Rhinoplasty()
        {
            return View();
        }
        public ActionResult Liposuction()
        {
            return View();
        }
        public ActionResult BreastAugmentation()
        {
            return View();
        }
        public ActionResult Gynecomastia()
        {
            return View();
        }
        public ActionResult HairTransplant()
        {
            return View();
        }
        public ActionResult Blepharoplasty()
        {
            return View();
        }
        public ActionResult Abdominoplasty()
        {
            return View();
        }
        public ActionResult FaceNeckLifting()
        {
            return View();
        }
        public ActionResult ButtockAugmentation()
        {
            return View();
        }
        public ActionResult FemaleBreastSurgery()
        {
            return View();
        }
    }
}