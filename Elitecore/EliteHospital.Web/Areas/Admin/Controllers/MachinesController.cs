
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
    public class MachinesController : Controller
    {
        // GET: Admin/CovidBanner
        
        CMSMachinesRepository repository = new CMSMachinesRepository();
        public ActionResult Index()
        {
            List<tbl_Machines> banners = repository.GetAll();
            ViewData["CovidBannerList"] = banners;
            return View();
        }

        public ActionResult DeleteMachines(int CovidBannerId)
        {
            repository.Delete(CovidBannerId);
            return RedirectToAction("Index");
        }

        public ActionResult EditMachines(int CovidBannerId = 0)
        {
            MachinesViewModel bannerViewModel = new MachinesViewModel();
            if (CovidBannerId > 0)
            {
                tbl_Machines banner = repository.GetById(CovidBannerId);
                if (banner != null)
                {
                    bannerViewModel = new MachinesViewModel()
                    {
                        Id = banner.Id,
                        Image = banner.Image,
                        ImagePath = banner.ImagePath,    
                        FileName=banner.FileName,
                        FileContent = banner.FileContent
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
        public ActionResult EditMachines(tbl_Machines model)
        {
            if (ModelState.IsValid)
            {
               
                var _bannerimage = Request.Files["covidbannerimage"];
                var _pdfupload = Request.Files["pdfupload"];

                String FileExt = Path.GetExtension(_pdfupload.FileName).ToUpper();
                    Stream str = _pdfupload.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                    tbl_Machines banner = new tbl_Machines()
                    {
                        Id = model.Id,
                        Image = ConvertToBytes(_bannerimage),
                        ImagePath = model.ImagePath,
                        FileName = _pdfupload.FileName,
                        FileContent = FileDet,
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