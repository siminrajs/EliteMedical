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

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class DepartmentController : Controller
    {
        // GET: Admin/Department
        CMSDepartmentRepository repository;
        HMSAPIRespository hmsAPIRespository;
        public DepartmentController()
        {
            repository = new CMSDepartmentRepository();
            hmsAPIRespository = new HMSAPIRespository();
        }
        public ActionResult Index(bool FromMob = false)
        {
            List<Department> departments = repository.GetAll();
            ViewData["DepartmentList"] = departments;
            ViewBag.FromMob = FromMob;
            return View();
        }

        public List<EliteHospital.Data.APIRequestResponseModels.Response.Department> GetDepartments()
        {
            try
            {
                return hmsAPIRespository.GetAllDepartments();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditDepartment(string Departmentname = "", bool FromMob = false)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.FromMob = FromMob;
            if (Departmentname != "")
            {
                Department department = repository.GetByName(Departmentname);
                if (department != null)
                {
                    departmentViewModel = new DepartmentViewModel()
                    {
                        DepartmentName = department.DepartmentName,
                        DepartmentNameArabic = department.DepartmentNameArabic,
                        Description = department.Description,
                        LongDescription = department.LongDescription,
                        DescriptionArabic = department.DescriptionArabic,
                        DepartmentImagePath = department.DepartmentImagePath,
                        DepartmentImageMobPath = department.DepartmentImageMobPath,
                        DepartmentIconImagePath = department.DepartmentIconImagePath,
                        Status = department.Status,
                        FromMob = FromMob,
                        OrderNo = department.OrderNo
                    };
                }
            }
            List<EliteHospital.Data.APIRequestResponseModels.Response.Department> departments = GetDepartments();
            List<DropdownViewModel> departmentlist = departments.Select(p=> new DropdownViewModel()
            {
                 Name = p.DepartmentName
            }).ToList();
            ViewData["DepartmentList"] = departmentlist;

            List<DropdownViewModel> status = new List<DropdownViewModel>();
            status.Add(new DropdownViewModel() { Status = "Y", Name = "Active" });
            status.Add(new DropdownViewModel() { Status = "N", Name = "InActive" });
            ViewData["StatusList"] = status;
            ViewBag.FromMob = FromMob;
            return View(departmentViewModel);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [HttpPost]
        public ActionResult DeleteDepartment(string DepartmentName)
        {
            repository.Delete(DepartmentName);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _departmentimage = Request.Files["departmentimage"];
                var _departmenticonimage = Request.Files["departmentimageicon"];
                var _departmentimagemob = Request.Files["departmentimagemob"];

                Department banner = new Department()
                {
                    DepartmentName = model.DepartmentName,
                    DepartmentNameArabic = model.DepartmentNameArabic,
                    Description = model.Description,
                    LongDescription = model.LongDescription,
                    DescriptionArabic = model.DescriptionArabic,
                    Status = model.Status,
                    OrderNo = model.OrderNo
                };
                if (model.FromMob == false)
                {
                    banner.DepartmentImage = ConvertToBytes(_departmentimage);
                    banner.DepartmentImagePath = model.DepartmentImagePath;

                    banner.DepartmentIconImage = ConvertToBytes(_departmenticonimage);
                    banner.DepartmentIconImagePath = model.DepartmentIconImagePath;
                }
                else
                {
                    banner.DepartmentImageMob = ConvertToBytes(_departmentimagemob);
                    banner.DepartmentImageMobPath = model.DepartmentImageMobPath;
                }
                repository.Save(banner);
                return RedirectToAction("Index", new { FromMob = model.FromMob });
            }
            return View(model);
        }
    }
}