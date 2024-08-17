using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class EventViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        //public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        //public byte[] EventImages { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public bool FromMob { get; set; }
        public string Type { get; set; }
        //public string Status { get; set; }
        public ICollection<Image1> Images { set; get; }
        //public Nullable<int> OrderNo { get; set; }
        public string Title_Arabic { get; set; }
        public string EventImages { get; set; }
        public string Description_Arabic { get; set; }

    }

    public class Image1
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }
    }
    
}