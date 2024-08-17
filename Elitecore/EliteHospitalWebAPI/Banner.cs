using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI
{
    public partial class Banner
    {
        [Key]
        public int Id { get; set; }
        public string BannerTitle { get; set; }
        public string BannerTitleArabic { get; set; }
        public string BannerSubTitle { get; set; }
        public string BannerSubTitleArabic { get; set; }
        public byte[] BannerImage { get; set; }
        public byte[] BannerImageMobile { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerImageMobilePath { get; set; }
    }
}
