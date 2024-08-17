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
        public ActionResult Index(bool FromMob = false,bool Createdepart= false)
        {
            List<Department> departments = repository.GetAll();
            ViewData["DepartmentList"] = departments;
            ViewBag.FromMob = FromMob;
            ViewBag.Createdepart = Createdepart;
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

        public ActionResult EditDepartment(int DepartmentId = 0, bool FromMob = false,bool Createdepart=false)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.FromMob = FromMob;
            if (DepartmentId !=0)
            {

                Department department = repository.GetByName(DepartmentId);
                if (department != null)
                {
                    departmentViewModel = new DepartmentViewModel()
                    {
                        DepartmentName = department.DepartmentName,
                        DepartmentNameArabic = department.DepartmentNameArabic,
                        Description = department.Description,
                        LongDescription = department.LongDescription,
                        DescriptionArabic = department.DescriptionArabic,
                        DepartmentImagePath = "",
                        DepartmentImageMobPath = "",
                        DepartmentIconImagePath = "",
                        Department_Icon = department.Department_Icon,
                        Department_Image=department.Department_Image,
                        Status = department.Status,
                        FromMob = FromMob,
                        OrderNo = department.OrderNo
                    };
                }
            }
            if (@Createdepart == true)
            {
                List<Department> departments = repository.GetAll();
                ViewData["DepartmentList"] = departments;

            }
            else
            {
                List<EliteHospital.Data.APIRequestResponseModels.Response.Department> departments = GetDepartments();
                List<DropdownViewModel> departmentlist = departments.Select(p => new DropdownViewModel()
                {
                    Name = p.DepartmentName
                }).ToList();
                ViewData["DepartmentList"] = departmentlist;
            }
            List<DropdownViewModel> status = new List<DropdownViewModel>();
            status.Add(new DropdownViewModel() { Status = "Y", Name = "Active" });
            status.Add(new DropdownViewModel() { Status = "N", Name = "InActive" });
            ViewData["StatusList"] = status;
            ViewBag.FromMob = FromMob;
            ViewBag.Createdepart = Createdepart;
            return View(departmentViewModel);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult DeleteDepartment(int DepartmentId)
        {
            repository.Delete(DepartmentId);
            return RedirectToAction("Index", "Department");
            //return Json(new { success = true, Message = "Deleted successfully." });
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(DepartmentViewModel model, HttpPostedFileBase departmentimage, HttpPostedFileBase departmentimageicon)
        {

            Department department = repository.GetByName(model.DepartmentId);
            if (ModelState.IsValid)
            {
                //var _departmentimage = Request.Files["departmentimage"];
                //var _departmenticonimage = Request.Files["departmentimageicon"];
                //var _departmentimagemob = Request.Files["departmentimagemob"];


                string deptimage = "";
                if (departmentimage != null)
                {
                    string sname = UniquenameMoment();
                    string spath = "/UploadImages/Department/";
                    string spext = ".jpg";
                    deptimage = spath + sname + spext;

                    System.Web.Helpers.WebImage imagename = new System.Web.Helpers.WebImage(departmentimage.InputStream);
                    imagename.Resize(1000, 700, true, true); // Resizing the image on the fly...
                    imagename.Save(Server.MapPath("~" + spath) + sname + spext, imageFormat: "jpg", forceCorrectExtension: false);

                }
                else
                {
                    deptimage = department.Department_Image;
                }
                string departmenticonimage = "";
                if (departmentimageicon != null)
                {
                    string sname = UniquenameMoment();
                    string spath = "/UploadImages/Department/";
                    string spext = ".jpg";
                    departmenticonimage = spath + sname + spext;

                    System.Web.Helpers.WebImage imagename = new System.Web.Helpers.WebImage(departmentimageicon.InputStream);
                    imagename.Resize(1000, 700, true, true); // Resizing the image on the fly...
                    imagename.Save(Server.MapPath("~" + spath) + sname + spext, imageFormat: "jpg", forceCorrectExtension: false);

                }
                else
                {
                    departmenticonimage= department.Department_Icon;

                }

                Department banner = new Department()
                {
                    DepartmentId = model.DepartmentId,
                    DepartmentName = model.DepartmentName,
                    DepartmentNameArabic = model.DepartmentNameArabic,
                    Description = model.Description,
                    LongDescription = model.LongDescription,
                    DescriptionArabic = model.DescriptionArabic,
                    Status = model.Status,
                    OrderNo = model.OrderNo,
                    Department_Image= deptimage,
                    Department_Icon= departmenticonimage
                };
                //if (model.FromMob == false)
                //{
                //    banner.DepartmentImage = ConvertToBytes(_departmentimage);
                //    banner.DepartmentImagePath = model.DepartmentImagePath;

                //    banner.DepartmentIconImage = ConvertToBytes(_departmenticonimage);
                //    banner.DepartmentIconImagePath = model.DepartmentIconImagePath;
                //}
                //else
                //{
                //    banner.DepartmentImage = ConvertToBytes(_departmentimage);
                //    banner.DepartmentImagePath = model.DepartmentImagePath;
                //    //banner.DepartmentImageMob = ConvertToBytes(_departmentimagemob);
                //    //banner.DepartmentImageMobPath = model.DepartmentImageMobPath;

                //    banner.DepartmentImageMob = ConvertToBytes(_departmentimage);
                //    banner.DepartmentImageMobPath = model.DepartmentImagePath;

                //    //banner.DepartmentIconImage = ConvertToBytes(_departmenticonimage);
                //    banner.DepartmentIconImagePath = department.DepartmentIconImagePath;
                //}
                repository.Save(banner);
                return RedirectToAction("Index", new { FromMob = model.FromMob });
            }
            return View(model);
        }

        public string UniquenameMoment()
        {
            string Name = "";
            Name = "" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond;
            return Name;
        }

    }
}