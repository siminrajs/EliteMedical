using EliteHospital.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EliteHospital.Tests
{
    [TestClass]
    public class HMSApiTest
    {
        [TestMethod]
        public void GetAllDepartmentsTest()
        {
            HMSAPIRespository hmsAPIRespository = new HMSAPIRespository();
            //var data = hmsAPIRespository.GetAllDepartments();
        }

        [TestMethod]
        public void ValidatePatientTest()
        {
            HMSAPIRespository hmsAPIRespository = new HMSAPIRespository();
            string mobileNo = "12345673";
            string qatarId = "123423567";
            var data = hmsAPIRespository.ValidatePatient(mobileNo, qatarId);
        }

        [TestMethod]
        public void GetPatientDetailsTest()
        {
            HMSAPIRespository hmsAPIRespository = new HMSAPIRespository();
            int patientId = 23659;
            var data = hmsAPIRespository.GetPatientDetails(patientId);
        }
    }
}
