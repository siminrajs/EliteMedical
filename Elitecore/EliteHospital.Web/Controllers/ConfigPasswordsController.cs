using EliteHospital.Core;
using EliteHospital.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    public class ConfigPasswordsController : Controller
    {
        // GET: ConfigPasswords
        GetAdminData adminData;
        CMSConfigPasswordsRepository repository = new CMSConfigPasswordsRepository();
        public ConfigPasswordsController()
        {
            adminData = new GetAdminData();
        }
        public ActionResult Index()
        {
            ConfigPasswords configpass = adminData.GetConfigpasswords();
            if (configpass == null)
            {
                return HttpNotFound();
            }
            return View(configpass);
        }

    }
}