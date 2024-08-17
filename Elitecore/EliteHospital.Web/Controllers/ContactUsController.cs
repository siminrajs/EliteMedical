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
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        GetAdminData adminData;
        CMSContactusRepository repository = new CMSContactusRepository();
        
        public ContactUsController()
        {
            adminData = new GetAdminData();
        }
        public ActionResult Index()
        {
            ContactU contact = adminData.GetContactUs();
            if (contact != null)
            {
                ViewBag.WorkingHours = contact.WorkingHours;
                ViewBag.Email = contact.Email;
                ViewBag.Phone = contact.Phone;
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult SaveDetails(FormCollection form)
        {         
                tbl_Contact contactdetails = new tbl_Contact()
                {
                    FirstName = form["firstname"],
                    LastName = form["lastname"],
                    Mobile = form["mobile"],
                    Email = form["email"],
                    Subject = form["subject"],
                    Description= form["description"],
                    Datetime = DateTime.Now

                };
              repository.SaveContactDetails(contactdetails);
            Boolean res;
            var Username = contactdetails.FirstName;
            String Name = "EMC Enquiry";
            String email = Username.Trim();
            String Status = "";
            SmtpClient ss;
            MailMessage mailMsg;
            SendEmail(contactdetails.FirstName, contactdetails.Email, contactdetails.Subject, contactdetails.Description, out ss, out mailMsg);
            try
            {
                ss.Send(mailMsg);
                Status = "true";
                res = true;
                TempData["msg"] = "mail send";
            }
            catch (Exception ex)
            {
                Status = "false";
                res = false;
                TempData["emsg"] = "mail not send";
            }
            return RedirectToAction("Index");
        }
        private void SendEmail(string Name, string Email, String Subject,String Description, out SmtpClient ss, out MailMessage mailMsg)
        {
            //Email Details
            //email: elitemedicalmission @gmail.com
            //passcode : ktnomwqyheesmpdt
            //password: elite@2022
            //recovery mailid :developer @ociuz.com
            //recovery mobile: 7306821800
            //to mail :noreply@elitemedical.com.qa
            //pwrd:Elite@2022
            String Body = "<h2> Enquiry</h2><br> Name: " + Name + "<br>Email: " + Email + "<br>Message: " + Subject +"<br>"+Description;
            ss = GetEmailCredentials();
            mailMsg = new MailMessage();
            mailMsg.From = new MailAddress("elitemedicalmission@gmail.com", "EliteMedicalMission");
            mailMsg.ReplyToList.Add(new MailAddress("elitemedicalmission@gmail.com", "EliteMedicalMission"));
            mailMsg.To.Add(new MailAddress("noreply@elitemedical.com.qa", Name));
            mailMsg.Subject = Subject;
            mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            mailMsg.Body = Body;
            mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mailMsg.IsBodyHtml = true;
            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
            mailMsg.AlternateViews.Add(plainView);
            mailMsg.AlternateViews.Add(htmlView);
        }

        public SmtpClient GetEmailCredentials()
        {
            SmtpClient ss = new SmtpClient();
            ss.Host = "smtp.gmail.com";
            ss.Port = 587;
            ss.EnableSsl = true;
            ss.Timeout = 10000;
            ss.UseDefaultCredentials = true;
            // ss.Credentials = new NetworkCredential("spotit.reply@gmail.com", "nyghuvgyuscuipjs");
            ss.Credentials = new NetworkCredential("elitemedicalmission@gmail.com", "ktnomwqyheesmpdt");

            return ss;
        }
    }
}