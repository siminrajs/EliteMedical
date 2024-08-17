
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
    public class TermsAndConditionsController : Controller
    {
        // GET: Admin/TermsAndConditions
        CMSTermsAndConRepository repository = new CMSTermsAndConRepository();
        public ActionResult Index()
        {
            List<tbl_TermsAndConditions_Admin> termsAndcondilist = repository.GetAll();
            ViewData["TermsAndConditionsList"] = termsAndcondilist;
            return View();
        }

        //[HttpPost]
        public ActionResult DeleteTermsAndConditions(int Id)
        {
            repository.Delete(Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditTermsAndConditions(int Id = 0)
        {
            TermsAndConditionsViewModel viewModel = new TermsAndConditionsViewModel();
            if (Id > 0)
            {
                tbl_TermsAndConditions_Admin termsAndCon = repository.GetById(Id);
                if (termsAndCon != null)
                {
                    viewModel = new TermsAndConditionsViewModel()
                    {
                        TC_Id = termsAndCon.TC_Id,
                        TC_Description = termsAndCon.TC_Description,
                        TC_Description_Arabic = termsAndCon.TC_Description_Arabic,
                    };
                }
            }
            return View(viewModel);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditTermsAndConditions(TermsAndConditionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                tbl_TermsAndConditions_Admin termsAndCon = new tbl_TermsAndConditions_Admin()
                {
                    TC_Id = model.TC_Id,
                    TC_Description = model.TC_Description,
                    TC_Description_Arabic = model.TC_Description_Arabic,

                };


                if (model.TC_Id > 0)
                {
                    repository.Update(termsAndCon);
                }
                else
                {
                    repository.Save(termsAndCon);
                }

                return RedirectToAction("Index");
            }
            return View(model);

        }
    }
}