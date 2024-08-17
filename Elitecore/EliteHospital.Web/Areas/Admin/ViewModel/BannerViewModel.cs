using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string BannerTitle { get; set; }
        //[Required]
        //public string BannerTitleArabic { get; set; }
        [Required]
        public string BannerSubTitle { get; set; }
        //[Required]
        //public string BannerSubTitleArabic { get; set; }
        public string BannerImage { get; set; }
        public string BannerImageMobile { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerImageMobilePath { get; set; }
        public bool FromMob { get; set; }
        public string DepartmentName { get; set; }

        public string Banner_Image { get; set; }
    }
}