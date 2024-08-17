using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class MachinesViewModel
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }
    }
}