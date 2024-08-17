using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class PatientViewModel
    {
        public int PMId { get; set; }

        public string FistName { get; set; }

        public string LastName { get; set; }

        public Nullable<System.DateTime> DOB { get; set; }

        public string Genter { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string QID { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public Nullable<System.DateTime> LastUpdatedDate { get; set; }

        public Nullable<decimal> Price { get; set; }

        public string VerificationCode { get; set; }

        public string ServiceId { get; set; }

        public string Promocode { get; set; }

    }
}
