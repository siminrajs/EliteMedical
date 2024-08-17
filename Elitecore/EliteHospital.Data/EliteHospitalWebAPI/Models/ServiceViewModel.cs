using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{

    public class ServicePointList<T> : ResponseServiceData
    {
        public T data { get; set; }
    }
    public class ServiceListClass
    {
        public List<TransactionViewModel> Transcationlist { get; set; }
        public List<ServiceModel> Servicelist { get; set; }
    }
    public class ResponseServiceData
    {
        public bool IsSuccess { get; set; }
        public string Name { get; set; }
        public decimal BalancePoint { get; set; }
        public string Message { get; set; }
        public decimal MiniRedeemPoint { get; set; }
    }


  
    public class ServiceViewModel
    {
        public int SM_Id { get; set; }

        public string SM_Name { get; set; }

        public string SM_Points { get; set; }

        public Nullable<System.DateTime> SM_CreatedDate { get; set; }        
    }

    public class ServiceModel
    {
        public int SM_Id { get; set; }

        public string SM_Name { get; set; }

        public decimal SM_Points { get; set; }

        public Nullable<System.DateTime> SM_CreatedDate { get; set; }
    }
}
