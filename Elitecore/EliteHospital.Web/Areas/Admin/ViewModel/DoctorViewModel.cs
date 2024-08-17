using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string DoctorId { get; set; }
        [Required]
        public string DoctorName { get; set; }
    
        public string DoctorImage { get; set; }
        public string DoctorImagePath { get; set; }
        public string DoctorImageMob { get; set; }
        public string DoctorImageMobPath { get; set; }
        public bool FromMob { get; set; }
        public int OrderNo { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
        public string DoctorNameArabic { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string DoctorDescription { get; set; }
        public string DoctorBioStatus { get; set; }
        public string Doctor_Image { get; set; }
        public string DoctorDescription_Arabic { get; set; }
    }
}