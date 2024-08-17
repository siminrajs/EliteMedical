using System.ComponentModel.DataAnnotations;

namespace EliteHospital.Web.Areas.Admin.ViewModel
{
    public class HomeBannerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string ExploreUrl { get; set; }
    }
}