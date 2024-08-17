using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class TransactionViewModel
    {
        public Int64 TH_Id { get; set; }

        public Int64 TH_PM_Id { get; set; }

        public decimal TH_LoyaltyPoints { get; set; }

        public Nullable<System.DateTime> TH_AddedDate { get; set; }

        public decimal TH_RedeemPoints { get; set; }

        public Nullable<System.DateTime> TH_RedeemedDate { get; set; }

        public string TH_Narrations { get; set; }

        public string TH_Status { get; set; }
    }
}
