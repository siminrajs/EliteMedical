using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Request
{
    public class SaveAppointment
    {
        public string DoctorId { get; set; }
        public int PatId { get; set; }
        public string ConsultationOn { get; set; }
        public int IsBookForAnotherPerson { get; set; }
        public string BookingForName { get; set; }
        public char BookingForGender { get; set; }
        public string BookingForRelation { get; set; }
        public string  Date { get; set; }
        public string  Time { get; set; }
    }
}
