using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        public string BannerTitle { get; set; }
        public string BannerTitleArabic { get; set; }
        public string BannerSubTitle { get; set; }
        public string BannerSubTitleArabic { get; set; }
        public byte[] BannerImage { get; set; }
        public byte[] BannerImageMobile { get; set; }
    }
}
