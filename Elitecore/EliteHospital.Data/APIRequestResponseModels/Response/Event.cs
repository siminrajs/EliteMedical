using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Response
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public byte[] EventImages { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
