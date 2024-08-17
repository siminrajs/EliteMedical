
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
    public class UserController : Controller
    {
        // GET: Admin/SpecialOffer
        CMSUserRepository repository = new CMSUserRepository();
        public ActionResult Index()
        {
            List<User> userlist = repository.GetAll();
            ViewData["UserList"] = userlist;
            return View();
        }

        //[HttpPost]
        public ActionResult DeleteUser(int Id)
        {
            repository.Delete(Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditUser(int Id = 0)
        {
            UserViewModel viewModel = new UserViewModel();
            if (Id > 0)
            {
                User userlist = repository.GetById(Id);
                if (userlist != null)
                {
                    viewModel = new UserViewModel()
                    {
                        ID = userlist.ID,
                        UserName = userlist.UserName,
                        Password = userlist.Password,
                    };
                }
            }
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User userdetails = new User()
                {
                    ID = model.ID,
                    Name = "User",
                    UserName = model.UserName,
                    Password = model.Password,
                    Status = "U"
                };


                if (model.ID > 0)
                {
                    repository.Update(userdetails);
                }
                else
                {
                    repository.Save(userdetails);
                }

                return RedirectToAction("Index");
            }
            return View(model);
       
        }
    }
}