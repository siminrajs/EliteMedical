using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class AboutUsViewModel
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string OurVision { get; set; }
        public string OurMission { get; set; }
        public string LongDescription { get; set; }
        public string Image { get; set; }
    }
}
