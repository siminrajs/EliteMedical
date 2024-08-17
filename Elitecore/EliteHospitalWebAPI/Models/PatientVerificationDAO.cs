using EliteHospital.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class PatientVerificationDAO
    {
        private readonly EliteHospitalContext context;
        public PatientVerificationDAO(EliteHospitalContext _context)
        {
            context = _context;
        }
        //public PatientVerificationDAO(IConfiguration config)
        //{
        //    context = new EliteHospitalContext(config);
        //}
        public void SaveVerificationCode(string mobileNo, int verificationCode)
        {
            var record = context.PatientVerifications.Where(p => p.MobileNo == mobileNo).FirstOrDefault();
            var expiringOnUTC = DateTime.UtcNow.AddMinutes(10);
            if (record != null)
            {
                record.VerificationCode = verificationCode;
                record.ExpiringOnUtc = expiringOnUTC;
                record.RetryAttempts = 0;
            }
            else
            {
                PatientVerification patientVerification = new PatientVerification()
                {
                    MobileNo = mobileNo,
                    VerificationCode = verificationCode,
                    ExpiringOnUtc = expiringOnUTC,
                    RetryAttempts = 0,
                    CreatedOnUtc = DateTime.UtcNow
                };
                context.PatientVerifications.Add(patientVerification);
            }

            context.SaveChanges();
        }

        public ResponseData VerifyCode(string mobileNo, int verificationCode)
        {
            ResponseData response = new ResponseData();
            var record = context.PatientVerifications.Where(p => p.MobileNo == mobileNo).FirstOrDefault();
            if (record == null)
            {
                response.IsSuccess = false;
                response.Message = "Verification code is not yet generated for this phone number";
            }
            else
            {
                if (record.RetryAttempts <= 5)
                {
                    if (record.VerificationCode == verificationCode)
                    {
                        if (DateTime.UtcNow <= record.ExpiringOnUtc)
                        {
                            response.IsSuccess = true;
                            response.Message = "Successfully verified";

                            record.ExpiringOnUtc = null;
                            record.LastVerifiedOnUtc = DateTime.UtcNow;
                            record.RetryAttempts = 0;
                            record.VerificationCode = null;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Verification code expired. Please create new verification code";
                        }
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Wrong verification code";
                        record.RetryAttempts = record.RetryAttempts++;
                    }
                    context.SaveChanges();
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Attempted maximum retry attempts. Please create new verification code.";
                }


            }

            return response;
        }
    }
}
