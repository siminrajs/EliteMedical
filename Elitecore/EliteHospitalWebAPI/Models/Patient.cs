using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class Patient
    {
        public int PatId { get; set; }
        public string PatName { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
