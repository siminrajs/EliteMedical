using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class DailyOfferViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public string ImageMob { get; set; }
        public string ImageMobPath { get; set; }
        public bool FromMob { get; set; }
    }
}