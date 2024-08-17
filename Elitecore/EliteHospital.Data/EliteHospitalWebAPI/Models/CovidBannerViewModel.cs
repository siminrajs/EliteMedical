using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class CovidBannerViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
    }
}
