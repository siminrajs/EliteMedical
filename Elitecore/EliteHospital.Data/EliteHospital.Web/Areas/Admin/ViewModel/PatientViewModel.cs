using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class PatientViewModel
    {
        public int PM_Id { get; set; }

        public string PM_FistName { get; set; }

        public string PM_LastName { get; set; }

        public Nullable<System.DateTime> PM_DOB { get; set; }

        public string PM_Genter { get; set; }

        public string PM_Email { get; set; }

        public string PM_Mobile { get; set; }

        public string PM_QID { get; set; }

        public Nullable<System.DateTime> PM_CreatedDate { get; set; }

        public Nullable<System.DateTime> PM_LastUpdatedDate { get; set; }

        public Nullable<decimal> PM_Price { get; set; }
    }
}