using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class LoyaltyViewModel
    {
        public int TH_Id { get; set; }

        public int TH_PM_Id { get; set; }

        public string TH_LoyaltyPoints { get; set; }

        public Nullable<System.DateTime> TH_AddedDate { get; set; }

        public string TH_RedeemPoints { get; set; }

        public Nullable<System.DateTime> TH_RedeemedDate { get; set; }

        public string TH_Narrations { get; set; }

        public string TH_Status { get; set; }
    }
}