using EliteHospital.Core;
using EliteHospital.Web.Models;
using System.Web.Mvc;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace EliteHospital.Web.Controllers
{
    public class MachinesController : Controller
    {
        CMSMachinesRepository repository = new CMSMachinesRepository();
        // GET: Machines
        GetAdminData adminData;
        public MachinesController()
        {
            adminData = new GetAdminData();
        }
        public ActionResult Index()
        {
            List<tbl_Machines> banners = repository.GetAll();
            ViewData["MachinesList"] = banners;
          
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View();
        }

        [HttpGet]
        public ActionResult DownLoadFile(int id)
        {
            List<tbl_Machines> banners = repository.GetForDownload(id);
 
            var FileById = (from FC in banners
                            where FC.Id.Equals(id)
                            select new { FC.FileName, FC.FileContent }).FirstOrDefault();

                return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }

        
    }
}