using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.HMSAPIHelpers;
using EliteHospital.Data.Repository;
using EliteHospitalWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Server;



namespace EliteHospitalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {

        public string WebRootPath = ""; //Website absolute path
   
        private readonly IConfiguration config;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string BaseUrl = string.Empty;
        public LoyaltyController(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            _config = configuration;
            config = configuration;
            BaseUrl = _config["HMSApiBaseUrl"];
            _webHostEnvironment = webHostEnvironment;
            WebRootPath = _webHostEnvironment.WebRootPath;
        }
        [HttpGet]
        [Route("GetPatientdetailid/")]
        public LoyaltyPointList<TransactionViewModel> GetPatientdetailid(string Id)
        {
            LoyaltyPointList<TransactionViewModel> result = new LoyaltyPointList<TransactionViewModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@PatientId", Id));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetTransactionList", sqlParams);
                    string Name = "";
                    string BalancePoints = "0.0";
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<TransactionViewModel> result1 = new List<TransactionViewModel>();
                        result1 = db.ConvertDataTableToListClass<TransactionViewModel>(dsResult.Tables[1]).ToList();
                        Name = dsResult.Tables[0].Rows[0]["Name"].ToString();
                        BalancePoints = Convert.ToString(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
                        result.Data = result1;                       
                    }          
                    result.IsSuccess = true;
                    result.Name = Name;
                    result.BalancePoint = Convert.ToDecimal(BalancePoints);
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("GetLoyaltyPointList/")]
        public ServicePointList<ServiceListClass> GetLoyaltyPointList(string Id)
        {
            ServicePointList<ServiceListClass> result = new ServicePointList<ServiceListClass>();
            ServicePointList<ServiceListClass> trData = new ServicePointList<ServiceListClass>();
            List<TransactionViewModel> Transcationlist = new List<TransactionViewModel>();
            List<ServiceModel> Servicelist = new List<ServiceModel>();
           
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@PatientId", Id));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetLoyaltyPointList", sqlParams);
                    decimal BalancePoints = 0;decimal MiniRedeemPoint1 = 0;
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<TransactionViewModel> result1 = new List<TransactionViewModel>();
                        result1 = db.ConvertDataTableToListClass<TransactionViewModel>(dsResult.Tables[1]).ToList();
                        List<ServiceModel> servicedetails = new List<ServiceModel>();
                        servicedetails = db.ConvertDataTableToListClass<ServiceModel>(dsResult.Tables[2]).ToList();                 
                        BalancePoints = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
                        MiniRedeemPoint1 = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["MiniRedeemPoint"].ToString());
                        Transcationlist = result1;
                        Servicelist = servicedetails;
                        ServiceListClass data = new ServiceListClass();
                        data.Servicelist = Servicelist;
                        data.Transcationlist = Transcationlist;
                        result.data = data;                          
                    }

                    result.IsSuccess = true;
                    result.Name = "";
                    result.BalancePoint = BalancePoints;
                    result.Message = "success";
                    result.MiniRedeemPoint = MiniRedeemPoint1;
                }
               
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
      
        //[HttpGet]
        //[Route("RedeemPoint/")]
        //public ResponseDataPromocode<TransactionViewModel> RedeemPoint(string PatientId, string ServiceId,string TransactionId)
        // {
        //    ResponseDataPromocode<TransactionViewModel > result = new ResponseDataPromocode<TransactionViewModel>();
        //    try
        //    {
        //        using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
        //        {
        //            //Generate Promocode
        //                //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        //                string numbers = "1234567890";
        //                string characters = "";
        //                characters = characters  + numbers;

        //                int length = 10;
        //                string Promocode = "EMC";
        //                for (int i = 0; i < length; i++)
        //                {
        //                    string character = string.Empty;
        //                    do
        //                    {
        //                        int index = new Random().Next(0, characters.Length);
        //                        character = characters.ToCharArray()[index].ToString();
        //                    } while (Promocode.IndexOf(character) != -1);
        //                Promocode += character;
        //                }
        //            List<SqlParameter> sqlParams = new List<SqlParameter>();
        //            sqlParams.Add(new SqlParameter("@PatientId", Convert.ToInt64(PatientId)));
        //            sqlParams.Add(new SqlParameter("@Promocode", Promocode));
        //            sqlParams.Add(new SqlParameter("@ServiceId", ServiceId)); 
        //            sqlParams.Add(new SqlParameter("@TransactionId",Convert.ToInt64(TransactionId)));
        //            DataSet dsResult = db.SqlDataSetResult("USP_UpdatePromocode", sqlParams);
        //            if (dsResult.Tables[0].Rows.Count > 0)
        //            {
        //                result.PatientId = Convert.ToInt64(dsResult.Tables[0].Rows[0]["PatientId"].ToString());
        //                result.Promocode = dsResult.Tables[0].Rows[0]["Promocode"].ToString();
        //                result.ServiceId = Convert.ToInt64(dsResult.Tables[0].Rows[0]["ServiceId"].ToString());
        //                result.BalancePoints = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
        //            }
        //            result.IsSuccess = true;
        //            result.Message = "success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = false;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}


        [HttpGet]
        [Route("RedeemPoint/")]
        public async Task<TransactionViewWithCodeModel> RedeemPoint(string PatientId, string ServiceId, string TransactionId)
        {
            TransactionViewWithCodeModel result = new TransactionViewWithCodeModel();
            //Task<TransactionViewModel> result = new Task<TransactionViewModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    //Generate Promocode
                    //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                    string numbers = "1234567890";
                    string characters = "";
                    characters = characters + numbers;

                    int length = 6;
                    string Promocode = "EMC";
                    for (int i = 0; i < length; i++)
                    {
                        string character = string.Empty;
                        do
                        {
                            int index = new Random().Next(0, characters.Length);
                            character = characters.ToCharArray()[index].ToString();
                        } while (Promocode.IndexOf(character) != -1);
                        Promocode += character;
                    }
                    string ooredooCustomerId = config["ooredooCustomerId"];
                    string ooredooUsername = config["ooredooUsername"];
                    string ooredooPassword = config["ooredooPassword"];
                    string ooredooOriginator = config["ooredooOriginator"];
                    
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@PatientId", Convert.ToInt64(PatientId)));
                    sqlParams.Add(new SqlParameter("@Promocode", Promocode));
                    sqlParams.Add(new SqlParameter("@ServiceId", ServiceId));
                    sqlParams.Add(new SqlParameter("@TransactionId", Convert.ToInt64(TransactionId)));
                    DataSet dsResult = db.SqlDataSetResult("USP_UpdatePromocode", sqlParams);
                    string promocode = ""; decimal BalancePoints = 0;
                    Int64 PtId =0; string ServcId = ""; string Mobile = "";
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        PtId = Convert.ToInt64(dsResult.Tables[0].Rows[0]["PatientId"].ToString());
                        promocode = dsResult.Tables[0].Rows[0]["Promocode"].ToString();
                        ServcId = (dsResult.Tables[0].Rows[0]["ServiceId"].ToString());
                        BalancePoints = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["BalancePoints"].ToString());
                        Mobile= (dsResult.Tables[0].Rows[0]["Mobile"].ToString());
                        OoredooServiceAPI service = new OoredooServiceAPI(ooredooUsername, ooredooPassword, ooredooCustomerId, ooredooOriginator);
                        var status = await service.SendMessageAsync(Mobile, $"Your Elite Points code is {Promocode}. Please show this voucher code at Elite reception to redeem your points.  Please note this will be valid for 30 days.");
                       // var status = await service.SendMessageAsync(Mobile, $"Your Elite Hospital verification OTP code is {Promocode} .Code valid for 10 minutes only, one time use.Please DO NOT share this OTP with anyone.");
                    }
                    result.Promocode = promocode;
                    result.PatientId = PtId;
                    result.ServiceId = ServcId;
                    result.BalancePoints = BalancePoints;
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }



        //[HttpGet]
        //[Route("GetTermsandConditions/")]
        //public ResponseTermsAndConditions<TermsAndConditionsViewModel> GetTermsandConditions()
        //{
        //    ResponseTermsAndConditions<TermsAndConditionsViewModel> result = new ResponseTermsAndConditions<TermsAndConditionsViewModel>();
        //    try
        //    {
        //        using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
        //        {
        //            List<SqlParameter> sqlParams = new List<SqlParameter>();
        //            DataSet dsResult = db.SqlDataSetResult("USP_GetTermsandConditionsList", sqlParams);
        //            if (dsResult.Tables[0].Rows.Count > 0)
        //            {
        //                List<TermsAndConditionsViewModel> list= new List<TermsAndConditionsViewModel>();
        //                list = db.ConvertDataTableToListClass<TermsAndConditionsViewModel>(dsResult.Tables[0]).ToList();
        //                result.Data = list;
        //                result.IsSuccess = true;
        //                result.Message = "success";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = false;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}



        [HttpGet]
        [Route("GetTermsandConditions/")]
        public TermsAndConditionsList<TermsAndConditions> GetTermsandConditions()
        {
            TermsAndConditionsList<TermsAndConditions> result = new TermsAndConditionsList<TermsAndConditions>();
            TermsAndConditionsList<TermsAndConditions> trData = new TermsAndConditionsList<TermsAndConditions>();
            List<TermsAndConditionsViewModel> TermsAndConditionsView = new List<TermsAndConditionsViewModel>();
            List<TermsAndConditionsListModel> TermsAndConditionsList = new List<TermsAndConditionsListModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    DataSet dsResult = db.SqlDataSetResult("USP_GetTermsandConditionsList", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<TermsAndConditionsViewModel> result1 = new List<TermsAndConditionsViewModel>();
                        result1 = db.ConvertDataTableToListClass<TermsAndConditionsViewModel>(dsResult.Tables[0]).ToList();
                        List<TermsAndConditionsListModel> result2 = new List<TermsAndConditionsListModel>();
                        result2 = db.ConvertDataTableToListClass<TermsAndConditionsListModel>(dsResult.Tables[1]).ToList();
                        TermsAndConditionsView = result1;
                        TermsAndConditionsList = result2;
                        TermsAndConditions data = new TermsAndConditions();
                        data.TermsAndConditionsview = TermsAndConditionsView;
                        data.TermsAndConditionsList = TermsAndConditionsList;
                        result.data = data;
                        result.IsSuccess = true;
                        result.Message = "success";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("GetTermsAndConNew/")]
        public TermsAndConditionsNew<TermsAndConditionsModel> GetTermsAndConNew()
        {
            TermsAndConditionsNew<TermsAndConditionsModel> result = new TermsAndConditionsNew<TermsAndConditionsModel>();
            try
            {
                using (EliteLibrary.DAL Odb = new EliteLibrary.DAL(_config))
                {

                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    DataSet dsResult = Odb.SqlDataSetResult("USP_GetTermsandConditionsList_New", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                        

                    {
                        List<TermsAndConditionsModel> termsandcon = new List<TermsAndConditionsModel>();
                        termsandcon = Odb.ConvertDataTableToListClass<TermsAndConditionsModel>(dsResult.Tables[0]).ToList();
                        result.Data = termsandcon;
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #region DoctorBioView

        [HttpGet]
        [Route("GetDoctorBioView/")]
        public DoctorBioList<DoctorBio> GetDoctorBioView(string doctorId)
        {
            DoctorBioList<DoctorBio> result = new DoctorBioList<DoctorBio>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@DoctorId", doctorId));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetDoctorDetails", sqlParams);
                    string DoctorId = "";
                    string Description = "",DoctorDescription_Arabic="";
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        DoctorId = dsResult.Tables[0].Rows[0]["DoctorId"].ToString();
                        Description = (dsResult.Tables[0].Rows[0]["Description"].ToString());
                        DoctorDescription_Arabic = (dsResult.Tables[0].Rows[0]["DoctorDescription_Arabic"].ToString());
                        result.DoctorId = DoctorId;
                        result.Descriprion = Description;
                        result.DoctorDescription_Arabic = DoctorDescription_Arabic;
                    }
                    result.IsSuccess = true;              
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion DoctorBioView


        #region DepartmentDetails
        [HttpGet]
        [Route("GetDepartmentDetails/")]
        public DepartmentList<DepartmentViewModel> GetDepartmentDetails()
        {
            DepartmentList<DepartmentViewModel> result = new DepartmentList<DepartmentViewModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    DataSet dsResult = db.SqlDataSetResult("USP_GetDepartmentListing", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<DepartmentViewModel> departmentlist = new List<DepartmentViewModel>();
                        departmentlist = db.ConvertDataTableToListClass<DepartmentViewModel>(dsResult.Tables[0]).ToList();
                        result.Data = departmentlist;
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

       
        #endregion DepartmentDetails

        #region DoctorDetails
        [HttpGet]
        [Route("GetDoctorDetails/")]
        public DoctorList<DoctorModel> GetDoctorDetails(string dept=null)
        {
            DoctorList<DoctorModel> result = new DoctorList<DoctorModel>();
            try
            {
                using (EliteLibrary.DAL db = new EliteLibrary.DAL(_config))
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@DeptName", dept));
                    DataSet dsResult = db.SqlDataSetResult("USP_GetDoctorListing", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<DoctorModel> doctorlist = new List<DoctorModel>();
                        doctorlist = db.ConvertDataTableToListClass<DoctorModel>(dsResult.Tables[0]).ToList();
                        result.Data = doctorlist;
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion DoctorDetails

        #region Account
        [HttpPost]
        [Route("GetProfilePhoto/")]
        public Account<Profile1> GetProfilePhoto(string patientid, IFormFile profilepic = null)
       {
            Account<Profile1> result = new Account<Profile1>();
            try
            {
                using (EliteLibrary.DAL Odb = new EliteLibrary.DAL(_config))
                {
                    String fileUrl = "", imgfileUrl = "";
                    if (profilepic != null)
                    {
                        int imgMaxWidth = 1000;
                        int imgMaxHeight = 1000;
                        string imgfilePath = "/ProfilePic/";
                        string filePath = "/inetpub/wwwroot/EliteHospitalAPI/UploadImages" + imgfilePath;
                        String fileID = Odb.GetUniqueID();
                        fileUrl = filePath + fileID + ".jpg";
                        WebRootPath = "";
                        imgfileUrl = "http://23.254.226.29:8082/" + imgfilePath + fileID + ".jpg";
                        Odb.SaveFile(postedfile: profilepic, fileUrl: fileUrl, filePath: WebRootPath + filePath.Replace("/", @"\"), WebRootPath, imgWidth: imgMaxWidth, imgHeight: imgMaxHeight);
                    }


                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@ProfilePic", imgfileUrl));
                    sqlParams.Add(new SqlParameter("@Patientid", patientid));
                    DataSet dsResult = Odb.SqlDataSetResult("USP_GetProfileDetails", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        AccountModel profiledts = new AccountModel();
                        profiledts = Odb.ConvertDataTableToClass<AccountModel>(dsResult.Tables[0]);
                        result.PatientId = profiledts.PatientId;
                        result.ProfilePic = profiledts.ProfilePic;
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("GetProfileDetails/")]
        public Account<Profile1> GetProfileDetails(string patientid)
        {
            Account<Profile1> result = new Account<Profile1>();
            try
            {
                using (EliteLibrary.DAL Odb = new EliteLibrary.DAL(_config))
                {
                   
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    sqlParams.Add(new SqlParameter("@Patientid", patientid));
                    DataSet dsResult = Odb.SqlDataSetResult("USP_GetProfileDetails", sqlParams);
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        AccountModel profiledts = new AccountModel();
                        profiledts = Odb.ConvertDataTableToClass<AccountModel>(dsResult.Tables[0]);
                        result.PatientId = profiledts.PatientId;
                        result.ProfilePic = profiledts.ProfilePic;
                    }
                    result.IsSuccess = true;
                    result.Message = "success";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }


        #endregion Account

        #region Models
        public class LoyaltyPointList<T> : ResponseData
        {
            public List<T> Data { get; set; }
        }

        public class ResponseData
        {
            public bool IsSuccess { get; set; }
            public string Name { get; set; }
            public decimal BalancePoint { get; set; }
            public string Message { get; set; }
        }

        public class ResponseDataPromocode<T> 
        {
            public Int64 PatientId { get; set; }
            public Int64 ServiceId { get; set; }
            public string Promocode { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public decimal BalancePoints { get; set; }
        }

        public class ResponseTermsAndConditions<T> : Responsetermsandconditionlist
        {
            public List<T> Data { get; set; }
        }
        public class Responsetermsandconditionlist
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        public class DoctorBioList<T> : DoctorBio
        {

            public string DoctorId { get; set; }
            public string Descriprion { get; set; }
            public string DoctorDescription_Arabic { get; set; }
        }

        public class DoctorBio
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        public class DepartmentList<T> : Response1
        {
            public List<T> Data { get; set; }
        }

        public class Response1
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        public class DepartmentViewModel
        {
            public string DepartmentName { get; set; }
            public string DepartmentNameArabic { get; set; }
            public string Description { get; set; }
            public string DescriptionArabic { get; set; }
            public string Department_Image { get; set; }
           
        }
     
        #endregion Models
    }
}
