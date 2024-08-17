using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Controllers
{
    [AreaAuthorize("Site")]
    public class RewardsController : Controller
    {
        // GET: Rewards
        public ActionResult Index()
        {
            return View();
        }
    }
}