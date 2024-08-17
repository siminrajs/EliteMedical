using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class DepartmentViewModel
    {
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameArabic { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string DescriptionArabic { get; set; }
        public string DepartmentImage { get; set; }
        public string DepartmentImageMob { get; set; }
        public string DepartmentImagePath { get; set; }
        public string DepartmentImageMobPath { get; set; }
        public string DepartmentIconImage { get; set; }
        public string DepartmentIconImagePath { get; set; }
        public bool FromMob { get; set; }
        public string Status { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string Department_Icon { get; set; }
        public string Department_Image { get; set; }
    }
}