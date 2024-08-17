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
    public class DailyOffersController : Controller
    {
        // GET: Admin/DailyOffers
        CMSDailyOffersRepository repository;
        public DailyOffersController()
        {
            repository = new CMSDailyOffersRepository();
        }
        public ActionResult Index(bool FromMob = false)
        {
            List<DailyOffer> dailyOffers = repository.GetAll();
            ViewData["DailyOfferList"] = dailyOffers;
            ViewBag.FromMob = FromMob;
            return View();
        }

        public ActionResult EditDailyOffer(int Id = 0, bool FromMob = false)
        {
            DailyOfferViewModel viewModel = new DailyOfferViewModel();
            viewModel.FromMob = FromMob;
            if (Id > 0)
            {
                DailyOffer record = repository.GetById(Id);
                if (record != null)
                {
                    viewModel = new DailyOfferViewModel()
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
        public ActionResult DeleteDailyOffer(int Id)
        {
            repository.Delete(Id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        [HttpPost]
        public ActionResult EditDailyOffer(DailyOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _image = Request.Files["image"];
                var _imagemob = Request.Files["imagemob"];

                DailyOffer record = new DailyOffer()
                {
                    Id = model.Id
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