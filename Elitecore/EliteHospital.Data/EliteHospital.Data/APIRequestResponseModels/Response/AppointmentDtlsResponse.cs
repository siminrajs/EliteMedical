using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.APIRequestResponseModels.Response
{
    public class AppointmentDtlsResponse
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorNameArabic { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentArabic { get; set; }
        public string Designation { get; set; }
        public DateTime AppointmentOn { get; set; }
        public string AppointmentDate
        {
            get
            {
                return this.AppointmentOn.ToString("MMM dd yyyy", CultureInfo.InvariantCulture);
            }
        }
        public string Session
        {
            get
            {
                return this.AppointmentTime.Contains("AM") == true ? "Morning" : "Afternoon";
            }
        }
        public string PatientName_Relation
        {
            get
            {
                if (this.IsBookForAnotherPerson == 1)
                {
                    return this.BookingForName + "(" + this.BookingForRelation + ")";
                }
                else
                {
                    return this.BookingForName;
                }
            }
        }
        public string AppointmentTime
        {
            get
            {
                return this.AppointmentOn.ToString("HH:mm tt", CultureInfo.InvariantCulture);
            }
        }
        public string AppointmentOnString
        {
            get
            {
                return this.AppointmentDate + " at " + this.AppointmentTime;
            }
        }
        public string BookingInfoString
        {
            get
            {
                return this.DoctorName + ", " + this.AppointmentOnString;
            }
        }
        public string PatientName { get; set; }
        public DateTime BookedOn { get; set; }
        public int IsBookForAnotherPerson { get; set; }
        public string BookingForName { get; set; }
        public string BookingForRelation { get; set; }
        public string BookingForGender { get; set; }
        public byte[] DoctorPhoto { get; set; }
        public int IsCancelled { get; set; }
        public string AppointmentStatus { get; set; }
        public string Status
        {
            get
            {
                return this.IsCancelled == 1 ? "Cancelled" : "Upcoming";
            }
        }

    }
}
