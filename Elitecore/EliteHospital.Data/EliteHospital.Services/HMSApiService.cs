using EliteHospital.Data;
using EliteHospital.Data.APIRequestResponseModels.Request;
using EliteHospital.Data.APIRequestResponseModels.Response;
using EliteHospital.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Services
{
    public class HMSApiService
    {
        private readonly HMSAPIRespository hmsAPIRespository;
        private readonly CMSValidatePatientRepository cmsValidatePatientRepository;
        public HMSApiService()
        {
            hmsAPIRespository = new HMSAPIRespository();
            cmsValidatePatientRepository = new CMSValidatePatientRepository();
        }

        public HMSApiService(bool IsFromWebAPI, string baseurl, string username, string password)
        {
            hmsAPIRespository = new HMSAPIRespository(IsFromWebAPI,baseurl,username, password);
            cmsValidatePatientRepository = new CMSValidatePatientRepository();
        }

        public Task<bool> SendVerificationCode(string mobileNo, string qatarId)
        {
            try
            {
                //TwilioSmsService twilioSmsService = new TwilioSmsService();
                Random generator = new Random();
                int code = generator.Next(100000, 1000000);
                cmsValidatePatientRepository.SaveVerificationCode(mobileNo, code);
                OoredooService service = new OoredooService();
                return service.SendMessageAsync(mobileNo, $"Your Elite Hospital verification OTP code is {code} . Code vaid for 10 minutes only, one time use. Please DO NOT share this OTP with anyone.");
                //twilioSmsService.SendMessage(mobileNo, $"Your Elite Hospital verification OTP code is {code} . Code vaid for 10 minutes only, one time use. Please DO NOT share this OTP with anyone.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ValidatePatientResponse>> ValidatePatientAndSendVerificationCodeAsync(string mobileNo, string qatarId)
        {
            var data = ValidatePatient(mobileNo, qatarId);
            if (data.IsSuccess)
            {
                //TwilioSmsService twilioSmsService = new TwilioSmsService();

                Random generator = new Random();
                int code = generator.Next(100000, 1000000);
                cmsValidatePatientRepository.SaveVerificationCode(mobileNo, code);
                OoredooService service = new OoredooService();
                var status = await service.SendMessageAsync(mobileNo, $"Your Elite Hospital verification OTP code is {code} . Code vaid for 10 minutes only, one time use. Please DO NOT share this OTP with anyone.");
                //twilioSmsService.SendMessage(mobileNo, $"Your Elite Hospital verification OTP code is {code} . Code vaid for 10 minutes only, one time use. Please DO NOT share this OTP with anyone.");

            }
            return data;
        }

        public Result<ValidatePatientResponse> ValidatePatient(string mobileNo, string qatarId)
        {
            var data = hmsAPIRespository.ValidatePatient(mobileNo, qatarId);
            return data;
        }

        public ResponseData VerifyCode(string mobileNo, int verificationCode)
        {
            var data = cmsValidatePatientRepository.VerifyCode(mobileNo, verificationCode);
            return data;
        }

        public Result<SignUpResponse> SignUp(string firstName, string lastName, string dob, string email, string gender, string mobileNo, string qatarId)
        {
            var signUpRequest = new SignUpRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dob,
                Email = email,
                Gender = gender,
                MobileNo = mobileNo,
                QatarId = qatarId
            };

            var data = hmsAPIRespository.SignUp(signUpRequest);

            return data;
        }

        public Result<Profile> GetPatientDetails(int patientId)
        {
            var data = hmsAPIRespository.GetPatientDetails(patientId);
            return data;
        }
    }
}
