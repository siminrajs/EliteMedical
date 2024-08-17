using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class APIInputs
    {
        public int PatId { get; set; }
        public string MobileNo { get; set; }
        public string QatarId { get; set; }
        public string DoctorId { get; set; }
        public string SlotDate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int IsBookForAnotherPerson { get; set; }
        public string BookingForName { get; set; }
        public char BookingForGender { get; set; }
        public string BookingForRelation { get; set; }
        public int AppointmentId { get; set; }
        public string DepartmentId { get; set; }
    }
}
