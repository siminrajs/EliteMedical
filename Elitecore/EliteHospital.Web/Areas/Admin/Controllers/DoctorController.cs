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
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctorsFromAPI = hmsAPIRespository.GetAllDoctors();

                EliteHospital.Data.APIRequestResponseModels.Response.Doctor doctorToUpdate = doctorsFromAPI.Where(p => p.DoctorId.Trim().ToUpper() == doctor.DoctorId.Trim().ToUpper()).FirstOrDefault();


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
                        Position = doctor.Position,
                        Description1 = doctor.Description1,
                        Description2 = doctor.Description2,
                        Description3 =doctor.Description3,
                        DoctorDescription=doctor.DoctorDescription,
                        Doctor_Image =doctor.Doctor_Image,
                        DoctorDescription_Arabic = doctor.DoctorDescription_Arabic,
                    };
                }
            }
            List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctorslistApi = GetDoctors();
            ViewData["DoctorListAPI"] = doctorslistApi;
            List<Doctor> doctors = repository.GetAll();
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

        public ActionResult DeleteDoctor(int Id)
        {
            Doctor doctor = repository.GetById(Id);
           
            //string FileName = doctor.Doctor_Image;
            //if (FileName!=null)
            //{
            //    string Path = "http://www.emcqtr.com" + FileName;
            //    FileInfo file = new FileInfo(Path);
            //    if (file.Exists)
            //    {
            //        file.Delete();
            //    }
            //}
               
            repository.Delete(Id);
            return RedirectToAction("Index", "Doctor");

        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctor(DoctorViewModel model,HttpPostedFileBase doctorimage)
        {
            if (ModelState.IsValid)
            {

                string dctrimage = "";
                if (doctorimage!= null)
                {
                    string sname = UniquenameMoment();
                    string spath = "/UploadImages/Doctor/";
                    string spext = ".jpg";
                    dctrimage = spath + sname + spext;

                    System.Web.Helpers.WebImage imagename = new System.Web.Helpers.WebImage(doctorimage.InputStream);
                    imagename.Resize(1000, 700, true, true); // Resizing the image on the fly...
                    imagename.Save(Server.MapPath("~" + spath) + sname + spext, imageFormat: "jpg", forceCorrectExtension: false);
                }
               
                else if(model.Id > 0)
                {
                    Doctor doctor = repository.GetById(model.Id);
                    dctrimage = doctor.Doctor_Image;
                }
                var _doctorimage = Request.Files["doctorimage"];
                var _doctorimagemob = Request.Files["doctorimagemob"];
                //List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> deptDoctors = adminData.GetDoctors(dept);
                //ViewData["DeptDoctors"] = deptDoctors;
                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctorsFromAPI = hmsAPIRespository.GetAllDoctors();

                EliteHospital.Data.APIRequestResponseModels.Response.Doctor doctorToUpdate = doctorsFromAPI.Where(p => p.DoctorId.Trim().ToUpper() == model.DoctorId.Trim().ToUpper()).FirstOrDefault();

                Doctor banner = new Doctor()
                {
                    Id = model.Id,
                    DoctorId = model.DoctorId,
                    DoctorName = doctorToUpdate.DoctorName,
                    OrderNo = model.OrderNo,
                    Status = model.Status,
                    Position = model.Position,
                    DoctorNameArabic = model.DoctorNameArabic,
                    Description1=model.Description1,
                    Description2=model.Description2,
                    Description3=model.Description3,
                    DoctorDescription =model.DoctorDescription,
                    DoctorBioStatus="INACTIVE",
                    Doctor_Image= dctrimage,
                    DepartmentName= doctorToUpdate.DepartmentName,
                    DoctorDescription_Arabic = model.DoctorDescription_Arabic,
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


        [HttpPost]
        public ActionResult UpdateBioStatus(int Id)
        {
            repository.UpdateDoctorBIoStatus(Id);
            return Json(new { success = true, Message = "status updated successfully." });

        }

        public string UniquenameMoment()
        {
            string Name = "";
            Name = "" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond;
            return Name;
        }
    }
}