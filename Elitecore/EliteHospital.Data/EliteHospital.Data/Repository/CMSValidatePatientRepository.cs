using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSValidatePatientRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSValidatePatientRepository()
        {
            context = new EliteHospitalEntities();
        }

        public void SaveVerificationCode(string mobileNo, int verificationCode)
        {
            var record = context.PatientVerifications.Where(p => p.MobileNo == mobileNo).FirstOrDefault();
            var expiringOnUTC = DateTime.UtcNow.AddMinutes(10);
            if (record != null)
            {
                record.VerificationCode = verificationCode;
                record.ExpiringOnUTC = expiringOnUTC;
                record.RetryAttempts = 0;
            }
            else
            {
                PatientVerification patientVerification = new PatientVerification() { MobileNo = mobileNo, VerificationCode = verificationCode, ExpiringOnUTC = expiringOnUTC, RetryAttempts = 0, CreatedOnUTC = DateTime.UtcNow };
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
                        if (DateTime.UtcNow <= record.ExpiringOnUTC)
                        {
                            response.IsSuccess = true;
                            response.Message = "Successfully verified";

                            record.ExpiringOnUTC = null;
                            record.LastVerifiedOnUTC = DateTime.UtcNow;
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
