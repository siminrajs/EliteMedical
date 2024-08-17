using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class ConfigPasswordsViewModel
    {
        public int ID { get; set; }
        [Required]
        public string ConfigPasswordName { get; set; }
        [Required]
        public string ConfigPassValues { get; set; }
    }
}