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
    public class AboutUsController : Controller
    {
        // GET: Admin/AboutUs
        CMSAboutusRepository repository = new CMSAboutusRepository();
        public ActionResult Index()
        {
            List<AboutU> aboutus = repository.GetAll();
            ViewData["AboutusList"] = aboutus;
            return View();
        }

        public ActionResult EditAboutUs(int Id = 0)
        {
            AboutusViewModel viewModel = new AboutusViewModel();
            if (Id > 0)
            {
                AboutU aboutus = repository.GetById(Id);
                if (aboutus != null)
                {
                    viewModel = new AboutusViewModel()
                    {
                        Id = aboutus.Id,
                        ShortDescription = aboutus.ShortDescription,
                        OurVision = aboutus.OurVision,
                        OurMission = aboutus.OurMission,
                        LongDescription = aboutus.LongDescription,
                        ImagePath = aboutus.ImagePath
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

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditAboutUs(AboutusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _image = Request.Files["aboutusimage"];

                AboutU about = new AboutU()
                {
                    Id = model.Id,
                    ShortDescription = model.ShortDescription,
                    OurMission = model.OurMission,
                    OurVision = model.OurVision,
                    LongDescription = model.LongDescription,
                    Image = ConvertToBytes(_image),
                    ImagePath = model.ImagePath,
                };
                repository.Save(about);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}