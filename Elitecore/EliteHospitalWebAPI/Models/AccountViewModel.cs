using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class AccountViewModel
    {
        public String ProfilePic { get; set; }
        public Int64 PatientId { get; set; }
    }
    public class Account<T> : Profile1
    {

        public String ProfilePic { get; set; }
        public Int64 PatientId { get; set; }
    }

    public class Profile1
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class AccountModel
    {
        public String ProfilePic { get; set; }
        public Int64 PatientId { get; set; }

    }
}
