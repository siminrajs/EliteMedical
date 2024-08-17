using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class InsuranceViewModel
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageMob { get; set; }
    }
}
