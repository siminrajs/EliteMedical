using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class DailyOffersViewModel
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageMob { get; set; }
    }
}
