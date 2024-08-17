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
    public class DoctorController : Controller
    {
        // GET: Admin/Doctor
        CMSDoctorRepository repository;
        HMSAPIRespository hmsAPIRespository;
        public DoctorController()
        {
            repository = new CMSDoctorRepository();
            hmsAPIRespository = new HMSAPIRespository();
        }
        public ActionResult Index(bool FromMob = false)
        {
            List<Doctor> doctors = repository.GetAll();
            ViewData["DoctorList"] = doctors;
            ViewBag.FromMob = FromMob;
            return View();
        }

        public List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> GetDoctors()
        {
            try
            {
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = hmsAPIRespository.GetAllDoctors();
                doctors.Insert(0, new Data.APIRequestResponseModels.Response.Doctor() { DoctorId = "", DoctorName = "--Select--" });
                return doctors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditDoctor(int Id = 0, bool FromMob = false)
        {
            DoctorViewModel viewModel = new DoctorViewModel();
            viewModel.FromMob = FromMob;
            if (Id > 0)
            {
                Doctor doctor = repository.GetById(Id);
                if (doctor != null)
                {
                    viewModel = new DoctorViewModel()
                    {
                        Id = doctor.Id,
                        DoctorId = doctor.DoctorId,
                        DoctorName = doctor.DoctorName,
                        DoctorImagePath = doctor.DoctorImagePath,
                        DoctorImageMobPath = doctor.DoctorImageMobPath,
                        FromMob = FromMob,
                        OrderNo = doctor.OrderNo != null ? doctor.OrderNo.Value : 0,
                        Status = doctor.Status != null ? doctor.Status : "Y",
                        DoctorNameArabic = doctor.DoctorNameArabic,
                        Position = doctor.Position
                    };
                }
            }
            List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = GetDoctors();
            ViewData["DoctorList"] = doctors;
            List<DropdownViewModel> status = new List<DropdownViewModel>();
            status.Add(new DropdownViewModel() { Status = "Y", Name = "Active" });
            status.Add(new DropdownViewModel() { Status = "N", Name = "InActive" });
            ViewData["StatusList"] = status;
            return View(viewModel);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [HttpPost]
        public ActionResult DeleteDoctor(int Id)
        {
            repository.Delete(Id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        [HttpPost]
        public ActionResult EditDoctor(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _doctorimage = Request.Files["doctorimage"];
                var _doctorimagemob = Request.Files["doctorimagemob"];

                Doctor banner = new Doctor()
                {
                    Id = model.Id,
                    DoctorId = model.DoctorId,
                    DoctorName = model.DoctorName,
                    OrderNo = model.OrderNo,
                    Status = model.Status,
                    Position = model.Position,
                    DoctorNameArabic = model.DoctorNameArabic
                    //DoctorImage = ConvertToBytes(_doctorimage),
                    //DoctorImageMob = ConvertToBytes(_doctorimagemob),
                    //DoctorImagePath = model.DoctorImagePath,
                    //DoctorImageMobPath = model.DoctorImageMobPath
                };
                if (model.FromMob == false)
                {
                    banner.DoctorImage = ConvertToBytes(_doctorimage);
                    banner.DoctorImagePath = model.DoctorImagePath;
                }
                else
                {
                    banner.DoctorImageMob = ConvertToBytes(_doctorimagemob);
                    banner.DoctorImageMobPath = model.DoctorImageMobPath;
                }
                repository.Save(banner);
                return RedirectToAction("Index", new { FromMob = model.FromMob });
            }
            List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctors = GetDoctors();
            ViewData["DoctorList"] = doctors;
            return View(model);
        }
    }
}