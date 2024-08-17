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
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;


namespace EliteHospital.Web.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class LoyaltyController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HMSApiService hmsApiService;
        public LoyaltyController()
        {
            hmsApiService = new HMSApiService();
        }
        // GET: Admin/LoyalityPoint
        CMSAddLoyalityPointRepository repository = new CMSAddLoyalityPointRepository();
        public ActionResult Index(string name=null,decimal purchaseamt=0)
        {
            List<tbl_PatientMaster> banners = repository.GetAll();
            ViewData["CovidBannerList"] = banners;
            ViewBag.Name = name;
            ViewBag.PurchasedAmount = purchaseamt;
            return View();
        }
        public ActionResult TransactionDetails(int Patientid = 0)
        {
            LoyaltyViewModel LoyaltyViewModel = new LoyaltyViewModel();
            if (Patientid > 0)
            {
                tbl_PatientMaster patientdetails = repository.Getpatientdetails(Patientid);
                List<tbl_TransactionHistory> transactiondetails = repository.GetTransactionAll(Patientid);           
                tbl_TransactionHistory balpont = repository.GetPointBal(Patientid);
                ViewBag.BalancePoints = patientdetails.PM_Price;
                ViewBag.name = patientdetails.PM_FistName;
                ViewBag.Qid = patientdetails.PM_QID;
                ViewBag.mobile = patientdetails.PM_Mobile;
                ViewBag.patientId = patientdetails.PM_Id;
                ViewBag.qtramount = patientdetails.PM_Price;
                double balamt;
                if (patientdetails.PM_Price > 0)
                {
                    tbl_CommonParameter cp = repository.Getcommondetails();
                    double percentage = Convert.ToDouble(cp.CP_Value);
                    balamt = (Convert.ToDouble(patientdetails.PM_Price) / 100) * percentage;
                    ViewBag.qtramt = balamt;
                }

                ViewData["TransactionList"] = transactiondetails;

                return View();
            }
            else
            {
                return View();
            }
               

        }
        #region AddPoint
        public ActionResult AddPoint(int PatientId,string Name)
        {
            ViewBag.name = Name;
            ViewBag.patientId = PatientId;
            return View();
        }       
        public ActionResult savepoints(FormCollection form)
        {
            double LoyaltyPoints;

            tbl_CommonParameter cp = repository.Addcommondetails();
            double point = Convert.ToDouble(cp.CP_Value);
            LoyaltyPoints = (Convert.ToDouble(form["purchasedamount"]) / 100) * point;
            tbl_TransactionHistory TransactionHistory = new tbl_TransactionHistory()
            {
                TH_PM_Id = Convert.ToInt32(form["patientid"]),
                TH_LoyaltyPoints = Convert.ToDecimal(LoyaltyPoints),
                TH_AddedDate = Convert.ToDateTime(form["addeddate"]),
                TH_PurchasedAmount = Convert.ToDecimal(form["purchasedamount"]),
                TH_Narrations = form["narrations"],
                //TH_QtrAmount = Convert.ToDecimal(form["points"]),
                TH_QtrAmount = Convert.ToDecimal(LoyaltyPoints),
                TH_Status = "Added",
            };
            repository.saveaddpoints(TransactionHistory);
            tbl_PatientMaster patientdetails = repository.Getpatientdetails(Convert.ToInt32(form["patientid"]));

            return RedirectToAction("/Index", new { name = patientdetails.PM_FistName, purchaseamt = TransactionHistory.TH_LoyaltyPoints });
        }
        public ActionResult getperamount(int pramount = 0, int patientid = 0)
        {
            double balamt; 
            if (pramount > 0)
            {
                tbl_CommonParameter cp = repository.GetLoyalityPoint();
                double point = Convert.ToDouble(cp.CP_Value);
                double percentage= (Convert.ToDouble(point) * 100 / 100);
                balamt = (Convert.ToDouble(pramount) / 100 * percentage);        
                return Json(balamt, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        #endregion AddPoint

        //public ActionResult redeempoints(FormCollection form)
        //{
        //    double balamt;
        //    tbl_CommonParameter cp = repository.Getcommondetails();
        //    double percentage = Convert.ToDouble(cp.CP_Value);
        //    balamt = (Convert.ToDouble(form["redeemamount"]) / 100) * percentage;
        //    tbl_TransactionHistory TransactionHistory = new tbl_TransactionHistory()
        //    {
        //        TH_PM_Id = Convert.ToInt32(form["patientid"]),
        //        TH_RedeemedDate = Convert.ToDateTime(form["redeemdate"]),
        //        TH_RedeemPoints = Convert.ToInt32(form["redeemamount"]),
        //        TH_QtrAmount = Convert.ToInt32(balamt),
        //        TH_Narrations = form["narrations"],
        //    };
        //    repository.redeempoints(TransactionHistory);
        //    return RedirectToAction("/Index");
        //}
      
        public ActionResult viewuserlistpartial(string keyword=null)
        {
            ViewBag.keyword = keyword;
            return View();
        }

        #region Service
        public ActionResult LoyaltyPointSettings()
        {
            List<tbl_CommonParameter> result = repository.GetCommonParamlist();
            ViewBag.RedeemMiniPoint = result[1].CP_Value;
            ViewBag.RedeemLoyaltyPoint = result[2].CP_Value;
            ViewBag.AddLoyaltyPoint = result[3].CP_Value;
            return View();
        }

        public ActionResult AddLoyaltyPointSettings(string type,decimal point=0)
        {
                tbl_CommonParameter CommonParameter = new tbl_CommonParameter()
                {
                    CP_Param = type,
                    CP_Value = Convert.ToString(point),
                };
                repository.saveloyaltypoints(CommonParameter);
            string msg = "Saved Successfully";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        #endregion Service

        #region Redeem
        public ActionResult RedeemPoint(int PatientId, string Name = null)
        {
            tbl_TransactionHistory cp = repository.GetPointBal(PatientId);
            ViewBag.name = Name;
            ViewBag.patientId = PatientId;
            ViewBag.BalancePoints = cp.TH_BalancePoints;
            return View();
        }
        public ActionResult checkpromocode(string promocode,int patientid)
        {
            tbl_TransactionHistory result = repository.Checkpromocode(promocode, patientid);
            tbl_PatientMaster patientdetails = repository.Getpatientdetails(patientid);
            if(result==null)
            {
                string msg = "Invalid Entry";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else if(patientdetails.PM_Promocode!= result.TH_Promocode)
            {
                string msg = "Already Exist";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RedeemPoint(FormCollection form)
        {
            tbl_TransactionHistory TransactionHistory = new tbl_TransactionHistory()
            {
                TH_PM_Id = Convert.ToInt32(form["patid"]),
                TH_RedeemedDate = Convert.ToDateTime(form["redeemdate"]),
                TH_Promocode = form["promocode"],
                TH_Status = "RedeemApproval",
            };
            repository.Updateredeempoints(TransactionHistory);
            tbl_PatientMaster patientdetails = repository.Getpatientdetails(Convert.ToInt32(form["patid"]));
            tbl_ServiceMaster servicedetails = repository.Getservicedetails(Convert.ToInt32(patientdetails.PM_ServiceId));
            return RedirectToAction("/Index", new { name = patientdetails.PM_FistName, purchaseamt = servicedetails.SM_Points });

        }
        #endregion Redeem

    }
}