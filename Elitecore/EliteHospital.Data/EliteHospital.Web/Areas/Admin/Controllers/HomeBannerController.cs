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
    public class HomeBannerController : Controller
    {
        // GET: Admin/HomeBanner
        CMSHomeBannerRepository repository = new CMSHomeBannerRepository();
        public ActionResult Index()
        {
            List<HomeBanner> banners = repository.GetAll();
            ViewData["HomeBannerList"] = banners;
            return View();
        }

        public ActionResult DeleteHomeBanner(int Id)
        {
            repository.Delete(Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditHomeBanner(int Id = 0)
        {
            HomeBannerViewModel bannerViewModel = new HomeBannerViewModel();
            if (Id > 0)
            {
                HomeBanner banner = repository.GetById(Id);
                if (banner != null)
                {
                    bannerViewModel = new HomeBannerViewModel()
                    {
                        Id = banner.Id,
                        Title = banner.Title,
                        Description = banner.Description,
                        ExploreUrl = banner.ExploreUrl,
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

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditHomeBanner(HomeBannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _bannerimage = Request.Files["homebannerimage"];

                HomeBanner banner = new HomeBanner()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    ExploreUrl = model.ExploreUrl,
                    Image = ConvertToBytes(_bannerimage),
                    ImagePath = model.ImagePath,
                };
                repository.Save(banner);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}