using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class TermsAndConditionsViewModel
    {
        public Int64 TC_Id { get; set; }

        public string TH_Description { get; set; }

        public string TH_Arabic { get; set; }

    }

    public class TermsAndConditionsListModel
    {
        public Int64 TC_Id { get; set; }

        public string TCL_Value { get; set; }

        public string TCL_Text{ get; set; }

        public string TCL_Value_Arabic { get; set; }

        public string TCL_Text_Arabic { get; set; }
    }





    public class TermsAndConditionsList<T> : ResponseServiceData
    {
        public T data { get; set; }
    }
    public class TermsAndConditions
    {
        public List<TermsAndConditionsViewModel> TermsAndConditionsview { get; set; }
        public List<TermsAndConditionsListModel> TermsAndConditionsList { get; set; }
    }

    //NewModel For Terms And Conditions
    public class TermsAndConditionsNew<T> : ResponseDataNew
    {
        public List<T> Data { get; set; }
    }
    public class ResponseDataNew
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class TermsAndConditionsModel
    {
        public Int64 TC_Id { get; set; }
        public string TC_Description { get; set; }
        public string TC_Description_Arabic { get; set; }
        
    }

}
