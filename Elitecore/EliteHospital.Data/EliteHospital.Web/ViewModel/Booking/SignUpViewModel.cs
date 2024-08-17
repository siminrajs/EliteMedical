using EliteHospital.Data.APIRequestResponseModels.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.ViewModel.Booking
{
    public class SignUpViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string QatarId { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int VerificationCode { get; set; }
        public string CountryCode { get; set; }
        public bool IsValidated { get; set; }
        public string MobWithCode
        {
            get
            {
                return CountryCode + MobileNo;
            }
        }
    }
}