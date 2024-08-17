
using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class MediaController : Controller
    {
        // GET: Admin/CovidBanner

        CMSMediaRepository repository = new CMSMediaRepository();
        public ActionResult Index()
        {
            List<tbl_Media> banners = repository.GetAll();
            ViewData["CovidBannerList"] = banners;
            
            return View();


        }

        public ActionResult DeleteMedia(int CovidBannerId)
        {
            repository.Delete(CovidBannerId);
            return RedirectToAction("Index");
        }

        public JsonResult getMedia(int Id)
        {
            List<tbl_MediaImagesList> ImagesList = repository.GetMediaImagelist(Id);
            //string imreBase64Data1 = Convert.ToBase64String(ImagesList[0].Image); 
            //string imgDataURL1= string.Format("data:image/png;base64,{0}", imreBase64Data1);          
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
            repository.DeleteImagebyid(id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        public ActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(tbl_Media model)
        {
            //if (ModelState.IsValid)
            //{
                tbl_Media banner = new tbl_Media()
                {
                    Id = model.Id,
                    Category = model.Category,
                    Category_Arabic=model.Category_Arabic,
                };                             
                    repository.Save(banner);             
                return RedirectToAction("Index");
            //}
            return View(model);
        }

        public ActionResult EditMedia(int CovidBannerId = 0)
        {
            MediaViewModel bannerViewModel = new MediaViewModel();
            if (CovidBannerId > 0)
            {
                tbl_Media banner = repository.GetById(CovidBannerId);
                if (banner != null)
                {
                    bannerViewModel = new MediaViewModel()
                    {
                        Id = banner.Id,
                        Category = banner.Category,
                        //Image = banner.Image,
                        //ImagePath = banner.ImagePath,
                        Title=banner.Title

                    };
                    //List<tbl_MediaImagesList> ImagesList = repository.GetMediaImagelist(CovidBannerId);
                    //ViewBag.Id = CovidBannerId;
                    //ViewData["MediaImagesList"] = ImagesList;
                }

            }
            List<tbl_Media> medialist = repository.GetAll();
            ViewData["medialist"] = medialist;
            //List<tbl_MediaImagesList> ImagesList1 = repository.GetMediaImagelist(CovidBannerId);
            ViewBag.Id = CovidBannerId;
            //ViewData["MediaImagesList"] = ImagesList1;

            ////demmy
            //List<tbl_MediaImagesList> ImagesList2 = repository.GetMediaImagelist(1);
            //string imreBase64Data = Convert.ToBase64String(ImagesList2[0].Image);
            //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            ////Passing image data in viewbag to view  
            //ViewBag.ImageData = imgDataURL;

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
        public ActionResult EditMedia(tbl_Media model, IEnumerable<HttpPostedFileBase> images=null)
        {
            if (ModelState.IsValid)
            {
                tbl_Media mediadetails = repository.GetCatDetails(model.Category);

                tbl_Media banner = new tbl_Media()
                {
                    Id = mediadetails.Id,
                    Category = model.Category,
                    Title=model.Title,
                    Category_Arabic=mediadetails.Category_Arabic,
                };
             
                    repository.Update(banner);

                    var MediaImagesListModel = new List<MediaImagesListModel>();
                    foreach (var image in images)
                    {
                        if (image != null)
                        {
                      
                            string pname =  UniquenameMoment();
                            string ppath = "/UploadImages/Media/";
                            string pext = ".jpg";
                            var mediaimage = ppath + pname + pext;
                       
                                System.Web.Helpers.WebImage image1 = new System.Web.Helpers.WebImage(image.InputStream);
                                image1.Resize(1000, 700, true, true); // Resizing the image on the fly...
                                /* image1.Resize(400, 650, true, true);*/ // Resizing the image on the fly...
                                image1.Save(Server.MapPath("~" + ppath) + pname + pext, imageFormat: "jpg", forceCorrectExtension: false);
                            
                        tbl_MediaImagesList img = new tbl_MediaImagesList();
                        img.Id = model.Id;
                        img.MediaId = banner.Id;
                        //img.Image = data;
                        //img.ImagePath = image.FileName;
                        img.Media_Image = mediaimage;
                        repository.AddMediaImage(img);

                        //using (var br = new BinaryReader(image.InputStream))
                        //{
                        //    var data = br.ReadBytes(image.ContentLength);
                        //     tbl_MediaImagesList img = new tbl_MediaImagesList();
                        //    img.Id = model.Id;
                        //    img.MediaId = banner.Id;
                        //    img.Image = data;
                        //    img.ImagePath = image.FileName;
                        //    repository.AddMediaImage(img);
                        //}

                    }
                    }
                
            }

            return RedirectToAction("Index");
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