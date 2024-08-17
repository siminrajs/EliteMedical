using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class SpecialOfferViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ImageMobPath { get; set; }
        public bool FromMob { get; set; }
    }
}