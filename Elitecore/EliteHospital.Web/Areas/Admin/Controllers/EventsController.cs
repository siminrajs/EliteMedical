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
    public class EventsController : Controller
    {
        CMSEventRepository repository;
        private readonly EliteHospitalEntities context;
        //context = new EliteHospitalEntities();
        HMSAPIRespository hmsAPIRespository;
        public EventsController()
        {

            repository = new CMSEventRepository();
            hmsAPIRespository = new HMSAPIRespository();
        }
        public ActionResult Index(bool FromMob = false)
        {
            List<Event> events = repository.GetAllList();
            ViewData["EventList"] = events;
            ViewBag.FromMob = FromMob;
            
            return View();

        }       
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult EditEvent(int Id = 0, bool FromMob = false)
        {
            EventViewModel viewModel = new EventViewModel();
            viewModel.FromMob = FromMob;
            if (Id > 0)
            {
                Event eventlist = repository.GetById(Id);
                if (eventlist != null)
                {
                    viewModel = new EventViewModel()
                    {
                        Id = eventlist.Id,
                        Title = eventlist.Title,
                        Description = eventlist.Description,
                        //ImagePath = eventlist.ImagePath,
                        CreatedDate= eventlist.CreatedDate,
                        Type= eventlist.Type,       
                        FromMob = FromMob,
                        Title_Arabic= eventlist.Title_Arabic,
                        Description_Arabic= eventlist.Description_Arabic,   
                        EventImages =eventlist.EventImages,
                    };
                }
            }
            List<DropdownViewModel> status = new List<DropdownViewModel>();
            status.Add(new DropdownViewModel() { Status = "Events", Name = "Events" });
            status.Add(new DropdownViewModel() { Status = "Blog", Name = "Blog" });
            ViewData["StatusList"] = status;
            ViewBag.Id = Id;
            return View(viewModel);

        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        //[HttpPost]
        public ActionResult EditEvent(EventViewModel model, HttpPostedFileBase singleimage, IEnumerable<HttpPostedFileBase> images)
        {
            string simage = "";
            if (singleimage!=null)
            {           
            string sname =  UniquenameMoment();
            string spath = "/UploadImages/Event/";
            string spext = ".jpg";
             simage = spath + sname + spext;

           System.Web.Helpers.WebImage imagename= new System.Web.Helpers.WebImage(singleimage.InputStream);
           imagename.Resize(1000, 700, true, true); // Resizing the image on the fly...
           imagename.Save(Server.MapPath("~" + spath) + sname + spext, imageFormat: "jpg", forceCorrectExtension: false);
            }
            Event banner = new Event()
                {
                Id = model.Id,
                Title = model.Title,
                Description =model.Description,
                CreatedDate=model.CreatedDate,
                Type=model.Type,
                EventImages= simage,
                Title_Arabic = model.Title_Arabic,
                Description_Arabic = model.Description_Arabic,
            };
            if (model.Id > 0)
            {
                repository.Update(banner);
            }
            else
            {
                repository.Save(banner);
            }

            var EventImagesList = new List<EventImagesList>();
            foreach (var image in images)
            {
                if (image != null)
                {

                    string pname = /*model.Title + "_ " +*/ UniquenameMoment();
                    string ppath = "/UploadImages/Event/";
                    string pext = ".jpg";
                    var mediaimage = ppath + pname + pext;

                    System.Web.Helpers.WebImage image1 = new System.Web.Helpers.WebImage(image.InputStream);
                    image1.Resize(1000, 700, true, true); // Resizing the image on the fly...
                    /* image1.Resize(400, 650, true, true);*/ // Resizing the image on the fly...
                    image1.Save(Server.MapPath("~" + ppath) + pname + pext, imageFormat: "jpg", forceCorrectExtension: false);

                    EventImagesList img = new EventImagesList();
                    img.Id = model.Id;
                    img.EventId = banner.Id;
                    img.Event_Image = mediaimage;
                    repository.AddEventImage(img);   
                }
            }
       
            return RedirectToAction("Index", new { FromMob = model.FromMob });
            List<Event> events = repository.GetAllList();
            ViewData["EventList"] = events;
            return View(model);
        }

        public ActionResult DeleteEvent(int CovidBannerId)
        {
            
            Event eventlist = repository.GetById(CovidBannerId);
            string FileName = eventlist.EventImages;
            string Path = "http://www.emcqtr.com" + FileName;
            //FileInfo file = new FileInfo(Path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            repository.Delete(CovidBannerId);

            return RedirectToAction("Index");
        }

        public JsonResult getEvents(int Id)
        {
            List<EventImagesList> ImagesList = repository.GetEventImagelist(Id);
            //string imreBase64Data1 = Convert.ToBase64String(ImagesList[0].Image);
            //string imgDataURL1 = string.Format("data:image/png;base64,{0}", imreBase64Data1);
            return Json(new { result = ImagesList }, JsonRequestBehavior.AllowGet);
        }
       
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult DeleteImage(int id)
        {
            repository.DeleteEventImagelist(id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }
        #region functions
        public string UniquenameMoment()
        {
            string Name = "";
            Name = "" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond;
            return Name;
        }

        public String GetHomeBaseURL()
        {
            String GiftHomeURL = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            return GiftHomeURL.EndsWith("/") ? GiftHomeURL.TrimEnd('/') : GiftHomeURL;
        }

        public DateTime DateTimeNow()
        {
            DateTime todaysDate = DateTime.Now;
            var gmTime = todaysDate.ToUniversalTime();
            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var gmTimeConverted = TimeZoneInfo.ConvertTime(gmTime, indianTimeZone);
            DateTime ResultDateTime = Convert.ToDateTime(gmTimeConverted);
            return ResultDateTime;
        }

        #endregion functions
    }
}