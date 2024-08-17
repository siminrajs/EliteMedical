using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public byte[] DoctorImage { get; set; }
        public string DoctorImagePath { get; set; }
        public byte[] DoctorImageMob { get; set; }
        public string DoctorImageMobPath { get; set; }
        public string Status { get; set; }
        public string DoctorNameArabic { get; set; }
    }
}
