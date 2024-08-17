using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Response
{
    public class Doctor
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Designation { get; set; }
        public string Speciaity { get; set; }
        public string PositionRole { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
    }
}
