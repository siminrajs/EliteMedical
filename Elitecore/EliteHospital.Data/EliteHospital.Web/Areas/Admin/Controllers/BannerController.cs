using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class BannerController : Controller
    {
        // GET: Admin/Banner

        CMSBannerRepository repository = new CMSBannerRepository();
        public ActionResult Index(bool FromMob = false)
        {
            List<Banner> banners = repository.GetAll();
            ViewData["BannerList"] = banners;
            ViewBag.FromMob = FromMob;
            return View();
        }

        public ActionResult DeleteBanner(int BannerId, bool FromMob)
        {
            repository.Delete(BannerId);
            return RedirectToAction("Index", new { FromMob = FromMob });
        }

        public ActionResult EditBanner(int BannerId = 0, bool FromMob = false)
        {
            BannerViewModel bannerViewModel = new BannerViewModel();
            bannerViewModel.FromMob = FromMob;
            if (BannerId > 0)
            {
                Banner banner = repository.GetById(BannerId);
                //Image bannerImage = ByteArrayToImage(banner.BannerImage);
                if (banner != null)
                {
                    bannerViewModel = new BannerViewModel()
                    {
                        Id = banner.Id,
                        BannerTitle = banner.BannerTitle,
                        //BannerTitleArabic = banner.BannerTitleArabic,
                        BannerSubTitle = banner.BannerSubTitle,
                        //BannerSubTitleArabic = banner.BannerSubTitleArabic,
                        BannerImagePath = banner.BannerImagePath,
                        BannerImageMobilePath = banner.BannerImageMobilePath,
                        FromMob = FromMob
                    };
                }
            }
            List<EliteHospital.Data.APIRequestResponseModels.Response.Department> departments = new DepartmentController().GetDepartments();
            List<DropdownViewModel> departmentlist = departments.Select(p => new DropdownViewModel()
            {
                Name = p.DepartmentName
            }).ToList();
            ViewData["DepartmentList"] = departmentlist;
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
        public ActionResult EditBanner(BannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _bannerimage = Request.Files["bannerimage"];
                var _bannerimagemob = Request.Files["bannerimagemob"];

                Banner banner = new Banner()
                {
                    Id = model.Id,
                    BannerTitle = model.BannerTitle,
                    DepartmentName = model.DepartmentName,
                    //BannerTitleArabic = model.BannerTitleArabic,
                    BannerSubTitle = model.BannerSubTitle,
                    //BannerSubTitleArabic = model.BannerSubTitleArabic,
                };
                if(model.FromMob == false)
                {
                    banner.BannerImage = ConvertToBytes(_bannerimage);
                    banner.BannerImagePath = model.BannerImagePath;
                }
                else
                {
                    banner.BannerImageMobile = ConvertToBytes(_bannerimagemob);
                    banner.BannerImageMobilePath = model.BannerImageMobilePath;
                }
                if(model.Id > 0)
                {
                    repository.Update(banner);
                }
                else
                {
                    repository.Save(banner);
                }
                
                return RedirectToAction("Index", new { FromMob = model.FromMob });
            }
            return View(model);
        }
    }
}