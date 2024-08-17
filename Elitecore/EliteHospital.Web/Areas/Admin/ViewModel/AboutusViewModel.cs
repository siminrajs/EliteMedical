using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class AboutusViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string OurVision { get; set; }
        [Required]
        public string OurMission { get; set; }
        public string LongDescription { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
    }
}