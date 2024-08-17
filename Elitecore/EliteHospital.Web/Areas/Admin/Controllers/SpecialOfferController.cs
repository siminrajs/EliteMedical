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
    public class SpecialOfferController : Controller
    {
        // GET: Admin/SpecialOffer
        CMSSpecialOfferRepository repository = new CMSSpecialOfferRepository();
        public ActionResult Index(bool FromMob = false)
        {
            List<SpecialOffer> specialOffers = repository.GetAll();
            ViewData["SpecialOfferList"] = specialOffers;
            ViewBag.FromMob = FromMob;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteSpecialOffer(int Id)
        {
            repository.Delete(Id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        public ActionResult EditSpecialOffer(int Id = 0, bool FromMob = false)
        {
            SpecialOfferViewModel viewModel = new SpecialOfferViewModel();
            viewModel.FromMob = FromMob;
            if (Id > 0)
            {
                SpecialOffer specialOffer = repository.GetById(Id);
                if (specialOffer != null)
                {
                    viewModel = new SpecialOfferViewModel()
                    {
                        Id = specialOffer.Id,
                        ImagePath = specialOffer.ImagePath,
                        ImageMobPath = specialOffer.ImageMobPath,
                        FromMob = FromMob
                    };
                }
            }
            ViewBag.FromMob = FromMob;
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
        public ActionResult EditSpecialOffer(SpecialOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var image = Request.Files["image"];
                var imagemob = Request.Files["imagemob"];

                SpecialOffer specialOffer = new SpecialOffer()
                {
                    Id = model.Id
                };
                if (model.FromMob == false)
                {
                    specialOffer.Image = ConvertToBytes(image);
                    specialOffer.ImagePath = model.ImagePath;
                }
                else
                {
                    specialOffer.ImageMob = ConvertToBytes(imagemob);
                    specialOffer.ImageMobPath = model.ImageMobPath;
                }
                if (model.Id > 0)
                {
                    repository.Update(specialOffer);
                }
                else
                {
                    repository.Save(specialOffer);
                }

                return RedirectToAction("Index", new { FromMob = model.FromMob });
            }
            return View(model);
        }
    }
}