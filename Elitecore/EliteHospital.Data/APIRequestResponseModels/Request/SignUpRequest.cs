using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Request
{
    public class SignUpRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string QatarId { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public int? VerificationCode { get; set; }
    }
}
