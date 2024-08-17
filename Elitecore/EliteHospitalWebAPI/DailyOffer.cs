using System.ComponentModel.DataAnnotations;

namespace EliteHospitalWebAPI
{
    public class DailyOffer
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageMob { get; set; }
        public string ImageMobPath { get; set; }
    }
}
