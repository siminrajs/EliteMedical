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
    public class ContactusController : Controller
    {
        // GET: Admin/Contactus
        CMSContactusRepository repository = new CMSContactusRepository();
        public ActionResult Index()
        {
            List<ContactU> contactus = repository.GetAll();
            ViewData["ContactList"] = contactus;
            return View();
        }

        public ActionResult EditContactus(int Id = 0)
        {
            ContactusViewModel viewModel = new ContactusViewModel();
            if (Id > 0)
            {
                ContactU contactus = repository.GetById(Id);
                if (contactus != null)
                {
                    viewModel = new ContactusViewModel()
                    {
                        Id = contactus.Id,
                        Address = contactus.Address,
                        Phone = contactus.Phone,
                        Email = contactus.Email,
                        WorkingHours = contactus.WorkingHours,
                        Location = contactus.Location,
                        AddressMob = contactus.AddressMob,
                        WorkingHoursMob = contactus.WorkingHoursMob,
                        AddressArabicMob = contactus.AddressArabicMob,
                        WorkingHoursArabicMob = contactus.WorkingHoursArabicMob
                    };
                }
            }
            return View(viewModel);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditContactus(ContactusViewModel model)
        {
            if (ModelState.IsValid)
            {
                ContactU contactus = new ContactU()
                {
                    Id = model.Id,
                    Address = model.Address,
                    Phone = model.Phone,
                    WorkingHours = model.WorkingHours,
                    Email = model.Email,
                    Location = model.Location,
                    AddressMob = model.AddressMob,
                    WorkingHoursMob = model.WorkingHoursMob,
                    AddressArabicMob = model.AddressArabicMob,
                    WorkingHoursArabicMob = model.WorkingHoursArabicMob
                };
                if (model.Id > 0)
                {
                    repository.Update(contactus);
                }
                else
                {
                    repository.Save(contactus);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}