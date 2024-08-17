 using EliteHospital.Services;
using EliteHospital.Web.ViewModel;
using EliteHospital.Web.ViewModel.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EliteHospital.Data.APIRequestResponseModels.Request;
using System.Threading.Tasks;

namespace EliteHospital.Web.Controllers
{
    public class AccountController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HMSApiService hmsApiService;
        public AccountController()
        {
            hmsApiService = new HMSApiService();
        }
        // GET: Account
        public ActionResult Index()
        {

            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated && Session["PatientFullName"] != null)
            {
                return RedirectToAction("Index", "Booking");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(LoginViewModel data)
        {
            if (ModelState.IsValid)
            {
                //var validationData = hmsApiService.ValidatePatient(data.MobileNo, data.QatarId);
                //if (validationData.IsSuccess)
                //{
                //    FormsAuthentication.SetAuthCookie(validationData.Data.FirstOrDefault().CustomerId.ToString(), true);
                //    Session["PatientFullName"] = validationData.Data.FirstOrDefault().CustomerName;
                //    Session["QatarId"] = data.QatarId;
                //    return RedirectToAction("Index", "Booking");
                //}
                //else
                //{
                //    ModelState.AddModelError("", validationData.Message);
                //}


                logger.Info("Inside login function");
                if (data.VerificationCode == null && data.IsValidated == false)
                {
                    logger.Info("Validating patient and sending verification code ");
                    var validationData = await hmsApiService.ValidatePatientAndSendVerificationCodeAsync(data.MobWithCode, data.QatarId);

                    if (validationData.IsSuccess)
                    {
                        data.IsValidated = true;
                        logger.Info("Validating patient and sending verification code is completed");
                    }
                    else
                    {
                        logger.Info("Error while validating and sending verification code");
                        ModelState.AddModelError("", validationData.Message);
                    }
                }
                else if (data.IsValidated == true)
                {
                    if (data.VerificationCode == null)
                        ModelState.AddModelError("VerificationCode", "Verification Code is required");
                    else
                    {
                        logger.Info("Verifying the code - started");
                        var verifyCodeResponse = hmsApiService.VerifyCode(data.MobWithCode, data.VerificationCode.Value);
                        if (verifyCodeResponse.IsSuccess)
                        {
                            logger.Info("Verifying code completed successfully");
                            var validatedData = hmsApiService.ValidatePatient(data.MobWithCode, data.QatarId);
                            FormsAuthentication.SetAuthCookie(validatedData.Data.FirstOrDefault().CustomerId.ToString(), true);
                            Session["PatientFullName"] = validatedData.Data.FirstOrDefault().CustomerName;
                            Session["PatientID"] = validatedData.Data.FirstOrDefault().CustomerId.ToString();
                            Session["QatarId"] = data.QatarId;
                            logger.Info("Login successful with Patient Id - " + Convert.ToString(Session["PatientID"]) + " and Qatar Id -" + Convert.ToString(Session["QatarId"]));
                            return RedirectToAction("Index", "Booking");
                        }
                        else
                        {
                            logger.Info("Verifying code failed. Error message - " + verifyCodeResponse.Message);
                            ModelState.AddModelError("", verifyCodeResponse.Message);
                        }

                    }
                }
            }
            return View(data);
        }

        public async Task<JsonResult> ResendOTPAsync(string mobile, string qatarId)
        {
            try
            {
                var validationData = await hmsApiService.ValidatePatientAndSendVerificationCodeAsync(mobile, qatarId);
                if (validationData.IsSuccess)
                {
                    logger.Info("Validating patient and sending verification code is completed");
                }
                else
                {
                    logger.Info("Error while validating and sending verification code");
                }
                return Json(new { success = true, Message = "Verification code send successfully." });
            }
            catch (Exception ex)
            {
                logger.Info("Exception while validating and sending verification code");
                logger.Error(ex);
                return Json(new { success = false, Message = "Verification code sending failed." });
            }
        }

        public JsonResult SendVerficationCode(string mobileNo, string qatarId)
        {
            try
            {
                logger.Info("Sending verification code started");
                hmsApiService.SendVerificationCode(mobileNo, qatarId);
                logger.Info("Sending verification code completed successfully.");
                return Json(new { success = true, Message = "Verification code send successfully." });
            }
            catch (Exception ex)
            {
                logger.Error("Error in SendVerficationCode function");
                logger.Error(ex);
                return Json(new { success = false, Message = "Verification code sending failed." });
            }
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel data)
       {
            if (ModelState.IsValid)
            {
                logger.Info("Sign up started");
                logger.Info(data);
                try
                {
                    logger.Info("Verifying the code and mobile number");
                    var verifyCodeResponse = hmsApiService.VerifyCode(data.MobWithCode, data.VerificationCode);
                    if (verifyCodeResponse.IsSuccess)
                    {
                        logger.Info("Verifying the code and mobile number completed successfully");
                        data.Gender = data.Gender == "True" ? "MALE" : "FEMALE";
                        logger.Info("Inserting patient details started");
                        var response = hmsApiService.SignUp(data.FirstName, data.LastName, data.DateOfBirth, data.Email,
                            data.Gender, data.MobileNo, data.QatarId);
                        if(response.IsSuccess == false)
                        {
                            logger.Info(response.Message);
                            data.IsValidated = true;
                            ModelState.AddModelError("", response.Message);
                        }
                        else
                        {
                            logger.Info("Inserting patient details completed successfully");

                            var validatedData = hmsApiService.ValidatePatient(data.MobWithCode, data.QatarId);
                            FormsAuthentication.SetAuthCookie(validatedData.Data.FirstOrDefault().CustomerId.ToString(), true);
                            Session["PatientFullName"] = validatedData.Data.FirstOrDefault().CustomerName;
                            Session["PatientID"] = validatedData.Data.FirstOrDefault().CustomerId.ToString();
                            Session["QatarId"] = data.QatarId;
                            logger.Info("Login successful with Patient Id - " + Convert.ToString(Session["PatientID"]) + " and Qatar Id -" + Convert.ToString(Session["QatarId"]));
                            return RedirectToAction("Index", "Booking");
                        }
                    }
                    else
                    {
                        logger.Info("Verifying the code and mobile number is failed");
                        data.IsValidated = true;
                        ModelState.AddModelError("", verifyCodeResponse.Message);
                    }
                }
                catch (Exception ex)
                {
                    logger.Info("Error while signup the user");
                    logger.Error(ex);
                }
            }
            return View(data);
        }

        public ActionResult SignUp()
        {
            SignUpViewModel data = new SignUpViewModel();
            return View(data);
        }
    }
}