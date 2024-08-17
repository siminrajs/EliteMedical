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
    public class CovidBannerController : Controller
    {
        // GET: Admin/CovidBanner
        CMSCovidBannerRepository repository = new CMSCovidBannerRepository();
        public ActionResult Index()
        {
            List<CovidBanner> banners = repository.GetAll();
            ViewData["CovidBannerList"] = banners;
            return View();
        }

        public ActionResult DeleteCovidBanner(int CovidBannerId)
        {
            repository.Delete(CovidBannerId);
            return RedirectToAction("Index");
        }

        public ActionResult EditCovidBanner(int CovidBannerId = 0)
        {
            CovidBannerViewModel bannerViewModel = new CovidBannerViewModel();
            if (CovidBannerId > 0)
            {
                CovidBanner banner = repository.GetById(CovidBannerId);
                if (banner != null)
                {
                    bannerViewModel = new CovidBannerViewModel()
                    {
                        Id = banner.Id,
                        Name = banner.Name,
                        ImagePath = banner.ImagePath
                    };
                }
            }
            return View(bannerViewModel);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [HttpPost]
        public ActionResult EditCovidBanner(CovidBannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _bannerimage = Request.Files["covidbannerimage"];

                CovidBanner banner = new CovidBanner()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Image = ConvertToBytes(_bannerimage),
                    ImagePath = model.ImagePath,
                };
                if (model.Id > 0)
                {
                    repository.Update(banner);
                }
                else
                {
                    repository.Save(banner);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}