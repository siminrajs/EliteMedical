using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class OnlineConsultationController : Controller
    {
        // GET: Admin/OnlineConsultation
        CMSOnlineConsultationRepository repository = new CMSOnlineConsultationRepository();
        public ActionResult Index()
        {
            List<OnlineConsultation> onlineConsultations = repository.GetAll();
            ViewData["OnlineConsultationList"] = onlineConsultations;
            return View();
        }

        public ActionResult DeleteOnlineConsultation(int Id)
        {
            repository.Delete(Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditOnlineConsultation(int Id = 0)
        {
            OnlineConsultationViewModel viewModel = new OnlineConsultationViewModel();
            //ViewData["VideoUrlList"] = new List<VideoUrl>();
            if (Id > 0)
            {
                OnlineConsultation onlineconsultation = repository.GetById(Id);
                if (onlineconsultation != null)
                {
                    viewModel = new OnlineConsultationViewModel()
                    {
                        Id = onlineconsultation.Id,
                        Description = onlineconsultation.Description,
                        Title = onlineconsultation.Title,
                        ImagePath = onlineconsultation.Path,
                        Urls = onlineconsultation.Url
                    };
                    ViewData["VideoUrlList"] = onlineconsultation.VideoUrls.ToList();
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

        public ActionResult AddVideoUrl(VideoUrl videoUrl)
        {
            try
            {
                int Id = repository.AddVideoUrl(videoUrl);
                return Json(new { success = true, Id=Id, Message = "URL added successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message });
            }
        }

        public ActionResult DeleteVideoUrl(int Id)
        {
            try
            {
                repository.DeleteVideoUrl(Id);
                return Json(new { success = true, Message = "URL deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult EditOnlineConsultation(OnlineConsultationViewModel model  )
        {
            
            if (ModelState.IsValid)
            {
                //model = JsonConvert.DeserializeObject<OnlineConsultationViewModel>(data);
                var _image = Request.Files["image"];

                OnlineConsultation item = new OnlineConsultation()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Image = ConvertToBytes(_image),
                    Path = model.ImagePath,
                    Url = model.Urls
                };
                if (model.Id > 0)
                {
                    repository.Update(item);
                }
                else
                {
                    repository.Save(item);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}