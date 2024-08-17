using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Response
{
    public class Profile
    {
        public string FullName { get; set; }
        public string QatarId { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }
}
