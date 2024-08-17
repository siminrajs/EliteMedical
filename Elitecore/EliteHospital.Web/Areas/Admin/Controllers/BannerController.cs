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
        CMSDepartmentRepository departmentRepository = new CMSDepartmentRepository();
        public ActionResult Index(bool FromMob = false)
        {
            List<Banner> banners = repository.GetAll();
            ViewData["BannerList"] = banners;
            ViewBag.FromMob = FromMob;
            return View();
        }

        public ActionResult DeleteBanner(int BannerId, bool FromMob)
        {
            Banner bannerdetails = repository.GetById(BannerId);
            string FileName = bannerdetails.Banner_Image;
            //string Path = "http://www.emc.auraqatar.com" + FileName;
            //FileInfo file = new FileInfo(Path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
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
                        Banner_Image = banner.Banner_Image,
                        //BannerSubTitleArabic = banner.BannerSubTitleArabic,
                        //BannerImagePath = banner.BannerImagePath,
                        //BannerImageMobilePath = banner.BannerImageMobilePath,
                        FromMob = FromMob
                    };
                }
            }
            List<Department> departments = departmentRepository.GetAll();
           // List<EliteHospital.Data.APIRequestResponseModels.Response.Department> departments = new DepartmentController().GetDepartments();
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
        public ActionResult EditBanner(BannerViewModel model,HttpPostedFileBase bannerimage)
        {
            if (ModelState.IsValid)
            {
                string bnrimage = "";
              

                string bnnrimage = "";
                if (bannerimage != null)
                {
                    string sname = UniquenameMoment();
                    string spath = "/UploadImages/Banner/";
                    string spext = ".jpg";
                    bnnrimage = spath + sname + spext;

                    System.Web.Helpers.WebImage imagename = new System.Web.Helpers.WebImage(bannerimage.InputStream);
                    imagename.Resize(1000, 700, true, true); // Resizing the image on the fly...
                    imagename.Save(Server.MapPath("~" + spath) + sname + spext, imageFormat: "jpg", forceCorrectExtension: false);

                }
                else
                {
                    Banner bannerdetails = repository.GetById(model.Id);
                    bnnrimage = bannerdetails.Banner_Image;
                }
                Banner banner = new Banner()
                {
                    Id = model.Id,
                    BannerTitle = model.BannerTitle,
                    DepartmentName = model.DepartmentName,
                    //BannerTitleArabic = model.BannerTitleArabic,
                    BannerSubTitle = model.BannerSubTitle,
                    //BannerSubTitleArabic = model.BannerSubTitleArabic,
                    Banner_Image = bnnrimage,
                };
                //if(model.FromMob == false)
                //{
                //    banner.BannerImage = ConvertToBytes(_bannerimage);
                //    banner.BannerImagePath = model.BannerImagePath;
                //}
                //else
                //{
                //    banner.BannerImageMobile = ConvertToBytes(_bannerimagemob);
                //    banner.BannerImageMobilePath = model.BannerImageMobilePath;
                //}
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
            //return View(model);
            return RedirectToAction("Index", new { FromMob = model.FromMob });

        }


        public string UniquenameMoment()
        {
            string Name = "";
            Name = "" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond;
            return Name;
        }
    }
}