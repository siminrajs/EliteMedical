using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using EliteHospital.Data.Repository;

namespace EliteHospital.Web.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        GetAdminData adminData;
        CMSDepartmentRepository repository;
        CMSDoctorRepository repositorydoc = new CMSDoctorRepository();
        public DepartmentController()
        {
            repository = new CMSDepartmentRepository();
            adminData = new GetAdminData();
        }

        public ActionResult Index()
        {
            List<Department> departments = repository.GetAll();
            ViewData["DepartmentList"] = departments;
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
        #region DepartmentMobile
        public ActionResult DepartmentMobileView()
        {
            List<Department> departments = repository.GetAll();
            ViewData["Departments"] = departments;
            //ViewData["Departments"] = adminData.GetDepartments();
            return View();
        }

        public ActionResult DepartmentdtlsMobileView(string dept)
        {
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            //if (dept == "DERMATOLOGY")
            //{
            //    return RedirectToAction("DermatologySkincareMobileApp", new { });
            //}
            //else if (dept == "PLASTIC SURGERY")
            //{
            //    return RedirectToAction("PlasticSurgeryForMobileApp", new { });
            //}
            //else
            //{
                //List<Department> departments = adminData.GetDepartments();
                List<Department> departments = repository.GetAll();
                Department department = new Department();
                department = departments.Where(p => p.DepartmentName.ToUpper() == dept.ToUpper()).FirstOrDefault();
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> deptDoctors = adminData.GetDoctors(dept);
                ViewData["DeptDoctors"] = deptDoctors;
                return View(department);

            //}
        }

        public ActionResult DermatologySkincareMobileApp()
        {
            return View();
        }

        public ActionResult PlasticSurgeryForMobileApp()
        {
            return View();
        }
        #endregion DepartmentMobile


        #region DepartmentMobileArabic
        public ActionResult DepartmentMobileArabicView()
        {
            List<Department> departments = repository.GetAll();
            ViewData["Departments"] = departments;
            return View();
        }

        public ActionResult DepartmentdtlsMobileArabicView(string dept)
        {

            //if (dept == "DERMATOLOGY")
            //{
            //    return RedirectToAction("DermatologySkincareMobileArabicApp", new { });
            //}
            //else if (dept == "PLASTIC SURGERY")
            //{
            //    return RedirectToAction("PlasticSurgeryForMobileArabicApp", new { });
            //}
            //else
            //{
                //List<Department> departmentslist = repository.GetAll();
                List<Department> departments = repository.GetAll();
                Department department = new Department();
                department = departments.Where(p => p.DepartmentName.ToUpper() == dept.ToUpper()).FirstOrDefault();
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> deptDoctors = adminData.GetDoctors(dept);
                ViewData["DeptDoctors"] = deptDoctors;
                return View(department);
            //}
        }




        public ActionResult DermatologySkincareMobileArabicApp()
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

        public ActionResult PlasticSurgeryForMobileArabicApp()
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
        #endregion DepartmentMobile


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
                return RedirectToAction("DermatologySkincare", new { });
            }
            else if (dept == "PLASTIC SURGERY")
            {
                return RedirectToAction("PlasticSurgery", new { });
            }
            else
            {
                List<Doctor> doctorsFromServer = repositorydoc.GetAll();
                ViewData["DoctorListFromServer"] = doctorsFromServer;

                List<Department> departments = adminData.GetDepartments();
                Department department = new Department();
                department = departments.Where(p => p.DepartmentName.ToUpper() == dept.ToUpper()).FirstOrDefault();
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> deptDoctors = adminData.GetDoctors(dept);
                ViewData["DeptDoctors"] = deptDoctors;

                Department departmentfromserver = repository.GetByDept(dept);
                return View(departmentfromserver);
            }
        }




        public ActionResult DermatologySkincare()
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

        public ActionResult SurgicalProcedures()
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

        public ActionResult PlasticSurgery()
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

        public ActionResult Rhinoplasty()
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

        public ActionResult Liposuction()
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

        public ActionResult BreastAugmentation()
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

        public ActionResult Gynecomastia()
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
        public ActionResult HairTransplant()
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

        public ActionResult Blepharoplasty()
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
      
        public ActionResult Abdominoplasty()
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
        public ActionResult FaceNeckLifting()
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
        public ActionResult ButtockAugmentation()
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
        public ActionResult FemaleBreastSurgery()
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

        #region ArabicPlasticSurgery
        public ActionResult ArabicRhinoplasty()
        {
            return View();
        }
        public ActionResult ArabicLiposuction()
        {
            return View();
        }
        public ActionResult ArabicBreastAugmentation()
        {
            return View();
        }
        public ActionResult ArabicGynecomastia()
        {
            return View();
        }
        public ActionResult ArabicHairTransplant()
        {
            return View();
        }
        public ActionResult ArabicBlepharoplasty()
        {
            return View();
        }
        public ActionResult ArabicAbdominoplasty()
        {
            return View();
        }
        public ActionResult ArabicFaceNeckLifting()
        {
            return View();
        }
        public ActionResult ArabicButtockAugmentation()
        {
            return View();
        }
        public ActionResult ArabicFemaleBreastSurgery()
        {
            return View();
        }
        #endregion ArabicPlasticSurgery
    }
}