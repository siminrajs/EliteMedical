using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class MediaViewModel
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public string Category_Arabic { get; set; }

        public string Title { get; set; }
    }

    public class MediaImagesListModel
    {

        public int Id { get; set; }

        public Nullable<int> MediaId { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public string Media_Image { get; set; }

    }
}