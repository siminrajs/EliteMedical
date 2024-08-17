namespace EliteHospital.Data.HMSAPIHelpers
{
    public static class APIConstants
    {
        public const string ValidatePatient = "PatientValidation/ValidatePatient";
        public const string SavePatientDetails = "PatientValidation/SavePatientDetails";
        public const string GetAllDepartments = "ConsultantDetails/GetAllDepartments";
        public const string GetDoctorSlots = "AppointmentDetails/GetAppointmentFreeSlotsByConsId";
        public const string GetAllDoctors = "ConsultantDetails/GetAllDoctors";
        public const string GetAppointmentSlots = "AppointmentDetails/GetAppointmentFreeSlotsByConsId";
        public const string SaveAppointmentDetails = "AppointmentDetails/SaveAppointmentDetails";
        public const string GetAllUpcomingAppointments = "AppointmentDetails/GetAllUpcomingAppointmentsByPatientId";
        public const string CancelAppointment = "AppointmentDetails/CancelAppointmentByAppId";
        public const string GetAppointmentDetails = "AppointmentDetails/GetAppointmentDetailsByAppId";
        //public const string GetAppointmentHistory = "AppointmentDetails/GetAppointmentHistoryByPatientId";
        public const string GetLabReports = "LabEntryDetails/GetLabReportsById";
        //public const string GetLabReportDetails = "LabEntryDetails/GetLabReportDetailsById";
        public const string GetPatientDetails = "PatientDetails/GetPatientDetailsByPatId";
    }
}
