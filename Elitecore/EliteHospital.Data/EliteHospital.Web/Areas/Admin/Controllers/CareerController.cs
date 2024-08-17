using EliteHospital.Core;
using EliteHospital.Data.Repository;
using EliteHospital.Web.Areas.Admin.ViewModel;
using EliteHospital.Web.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class CareerController : Controller
    {
        // GET: Admin/Career
        CMSCareersRepository repository = new CMSCareersRepository();
        public ActionResult Index()
        {
            List<Career> careers = repository.GetAll();
            ViewData["CareerList"] = careers;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCareer(int Id)
        {
            repository.Delete(Id);
            return Json(new { success = true, Message = "Deleted successfully." });
        }

        public ActionResult EditCareer(int Id = 0)
        {
            CareerViewModel viewModel = new CareerViewModel();
            if (Id > 0)
            {
                Career career = repository.GetById(Id);
                if (career != null)
                {
                    viewModel = new CareerViewModel()
                    {
                        Id = career.Id,
                        Vacancy = career.Vacancy,
                        PositionDescription = career.PositionDescription,
                        Email = career.Email
                    };
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditCareer(CareerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Career career = new Career()
                {
                    Id = model.Id,
                    Vacancy = model.Vacancy,
                    PositionDescription = model.PositionDescription,
                    Email = model.Email
                };
                if (model.Id > 0)
                {
                    repository.Update(career);
                }
                else
                {
                    repository.Save(career);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}