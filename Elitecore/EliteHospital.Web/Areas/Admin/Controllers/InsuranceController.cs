using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class InsuranceController : Controller
    {
        // GET: Admin/Insurance
        CMSInsuranceRepository repository;
        public InsuranceController()
        {
            repository = new CMSInsuranceRepository();
        }
        public ActionResult Index(bool FromMob = false)
        {
            List<Insurance> insurances = repository.GetAll();
            ViewData["InsuranceList"] = insurances;
            ViewBag.FromMob = FromMob;
            return View();
        }

        public ActionResult EditInsurance(int Id = 0, bool FromMob = false)
        {
            InsuranceViewModel viewModel = new InsuranceViewModel();
            viewModel.FromMob = FromMob;
            if (Id > 0)
            {
                Insurance record = repository.GetById(Id);
                if (record != null)
                {
                    viewModel = new InsuranceViewModel()
                    {
                        Id = record.Id,
                        ImagePath = record.ImagePath,
                        ImageMobPath = record.ImageMobPath,
                        FromMob = FromMob
                    };
                }
            }
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
        public ActionResult DeleteInsurance(int Id)
        {
            repository.Delete(Id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        [HttpPost]
        public ActionResult EditInsurance(InsuranceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _image = Request.Files["image"];
                var _imagemob = Request.Files["imagemob"];

                Insurance record = new Insurance()
                {
                    Id= model.Id
                };
                if (model.FromMob == false)
                {
                    record.Image = ConvertToBytes(_image);
                    record.ImagePath = model.ImagePath;
                }
                else
                {
                    record.ImageMob = ConvertToBytes(_imagemob);
                    record.ImageMobPath = model.ImageMobPath;
                }

                repository.Save(record);
                return RedirectToAction("Index", new { FromMob = model.FromMob });
            }
            return View(model);
        }
    }
}