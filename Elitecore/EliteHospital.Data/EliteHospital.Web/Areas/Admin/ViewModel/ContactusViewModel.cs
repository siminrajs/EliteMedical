using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class ContactusViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string WorkingHours { get; set; }
        public string Location { get; set; }
        public string AddressMob { get; set; }
        public string WorkingHoursMob { get; set; }
        public string AddressArabicMob { get; set; }
        public string WorkingHoursArabicMob { get; set; }
    }
}