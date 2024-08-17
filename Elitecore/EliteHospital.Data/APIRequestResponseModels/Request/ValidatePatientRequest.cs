using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Request
{
    public class ValidatePatientRequest
    {
        public string MobileNo { get; set; }
        public string QatarId { get; set; }
    }
}
