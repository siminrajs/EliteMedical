using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class DoctorModel
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Designation { get; set; }
        public string Speciaity { get; set; }
        public byte[] DoctorImage { get; set; }
        public byte[] DoctorImageMob { get; set; }
        public string Status { get; set; }
        public string DoctorNameArabic { get; set; }
        public string DepartmentNameArabic { get; set; }

    }
}
