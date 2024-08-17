using System;
using System.Collections.Generic;

#nullable disable

namespace EliteHospitalWebAPI
{
    public partial class PatientVerification
    {
        public int Id { get; set; }
        public string MobileNo { get; set; }
        public int? VerificationCode { get; set; }
        public DateTime? ExpiringOnUtc { get; set; }
        public int RetryAttempts { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastVerifiedOnUtc { get; set; }
    }
}
