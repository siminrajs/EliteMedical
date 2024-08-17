using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The User Name field is required")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
