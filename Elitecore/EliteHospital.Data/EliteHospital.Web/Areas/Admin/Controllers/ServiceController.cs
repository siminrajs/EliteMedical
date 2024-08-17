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
using EliteHospital.Services;
using EliteHospital.Web.ViewModel;
using EliteHospital.Web.ViewModel.Booking;
using System.Web.Security;
using EliteHospital.Data.APIRequestResponseModels.Request;
using System.Threading.Tasks;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class ServiceController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HMSApiService hmsApiService;
        public ServiceController()
        {
            hmsApiService = new HMSApiService();
        }
        CMSAddLoyalityPointRepository repository = new CMSAddLoyalityPointRepository();
        public ActionResult AddNewServices(int Id=0)
        {
            if(Id!=0)
            {
                tbl_ServiceMaster servicedetails = repository.Getservicedetails(Id);
                ViewBag.Id = servicedetails.SM_Id;
                ViewBag.Name = servicedetails.SM_Name;
                ViewBag.point = servicedetails.SM_Points;
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddNewServices(FormCollection form)
        {
            tbl_ServiceMaster servicedetails = new tbl_ServiceMaster()
            {      SM_Id=Convert.ToInt32(form["smid"]),
                SM_Name = form["servicename"],
                SM_Points = form["servicepoint"],
                SM_CreatedDate = DateTime.Now,
        };
            repository.saveservicemaster(servicedetails);
            return RedirectToAction("/ServiceList", new { name = servicedetails.SM_Name, servicepoint = servicedetails.SM_Points });

        }
        public ActionResult ServiceList(string name = null, decimal servicepoint = 0)
        {
            ViewBag.Name = name;
            ViewBag.ServicePoint = servicepoint;
            return View();
        }

        public ActionResult ServiceListPartial()
        { 
            List<tbl_ServiceMaster> ServiceList = repository.ServiceListPartial();
            ViewData["ServiceList"] = ServiceList;
             return View();
        }

        public ActionResult DeleteService(int Id = 0)
        {
            repository.DeleteService(Id);
            return RedirectToAction("/ServiceList");

        }
    }
}

