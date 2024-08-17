using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.ViewModel.Booking
{
    public class LoginViewModel
    {
        [Required]
        public string MobileNo { get; set; }
        public string MobWithCode
        {
            get
            {
                return CountryCode + MobileNo;
            }
        }

        [Required]
        public string QatarId { get; set; }
        public string CountryCode { get; set; }
        public bool IsValidated { get; set; }
        public int? VerificationCode { get; set; }

    }
}