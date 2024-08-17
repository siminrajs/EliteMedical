using EliteHospital.Data.ViewModel;
using EliteHospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly CMSService accountService;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AccountController()
        {
            accountService = new CMSService();
        }
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel credential)
        {
            if (ModelState.IsValid)
            {
                CMSUserViewModel user = accountService.GetCMSUserByCredential(credential);
                if (user != null && user.Status == "A")
                {
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                    Session["UserName"] = user.Name;
                    Session["userId"] = user.Id;
                    return RedirectToAction("Index", "Banner");
                }
                else if (user != null && user.Status == "I")
                {
                    ModelState.AddModelError("", "This user is not active. Please contact admin");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(credential);
        }

        public ActionResult LogOut()
        {
            logger.Info("Inside logout function");
            FormsAuthentication.SignOut();
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            Session.Abandon();
            logger.Info("Log out completed and returning to log in view");
            return RedirectToAction("Index", "Account");
        }
    }
}