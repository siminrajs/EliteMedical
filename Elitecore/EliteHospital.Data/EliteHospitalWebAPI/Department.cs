using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI
{
    public partial class Department
    {
        public string DepartmentName { get; set; }
        public string DepartmentNameArabic { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string DescriptionArabic { get; set; }
        public byte[] DepartmentImage { get; set; }
        public byte[] DepartmentImageMob { get; set; }
        public string DepartmentImagePath { get; set; }
        public string DepartmentImageMobPath { get; set; }
        public string  Status { get; set; }
        public Nullable<int> OrderNo { get; set; }
    }
}
