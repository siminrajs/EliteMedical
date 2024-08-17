using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI
{
    public partial class AboutU
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string OurVision { get; set; }
        public string OurMission { get; set; }
        public string LongDescription { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
    }
}
