using EliteHospital.Core;
using EliteHospital.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EliteHospital.Web.Models
{
    public class GetAdminData
    {
        public List<AboutU> GetAboutUs()
        {
            try
            {
                CMSAboutusRepository aboutusRepository = new CMSAboutusRepository();
                return aboutusRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContactU GetContactUs()
        {
            try
            {
                ContactU contactU = new ContactU();
                CMSContactusRepository contactusRepository = new CMSContactusRepository();
                List<ContactU> contactUs = contactusRepository.GetAll();
                if (contactUs != null)
                {
                    contactU = contactUs.FirstOrDefault();
                }
                return contactU;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfigPasswords GetConfigpasswords()
        {
            try
            {
                ConfigPasswords configpass = new ConfigPasswords();
                CMSConfigPasswordsRepository configpassRepository = new CMSConfigPasswordsRepository();
                List<ConfigPasswords> configpass1 = configpassRepository.GetAll();
                if (configpass1 != null)
                {
                    configpass = configpass1.FirstOrDefault();
                }
                return configpass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Career> GetCareers()
        {
            try
            {
                CMSCareersRepository careersRepository = new CMSCareersRepository();
                List<Career> careers = careersRepository.GetAll();
                return careers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialOffer> GetSpecialOffers()
        {
            try
            {
                CMSSpecialOfferRepository repository = new CMSSpecialOfferRepository();
                List<SpecialOffer> specialOffers = repository.GetAll();
                return specialOffers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OnlineConsultation GetOnlineConsultation()
        {
            try
            {
                OnlineConsultation onlineConsultation = new OnlineConsultation();
                CMSOnlineConsultationRepository onlineConsultationRepository = new CMSOnlineConsultationRepository();
                List< OnlineConsultation> onlineConsultations = onlineConsultationRepository.GetAll();
                if(onlineConsultations!= null)
                {
                    onlineConsultation = onlineConsultations.FirstOrDefault();
                }
                return onlineConsultation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Insurance> GetInsurances()
        {
            try
            {
                CMSInsuranceRepository insuranceRepository = new CMSInsuranceRepository();
                return insuranceRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> GetDoctors(string Dept = "")
        {
            try
            {
                HMSAPIRespository hmsAPIRespository = new HMSAPIRespository();
                CMSDoctorRepository repository = new CMSDoctorRepository();

                List<EliteHospital.Data.APIRequestResponseModels.Response.Doctor> doctorsFromAPI = hmsAPIRespository.GetAllDoctors();
                if(Dept != "")
                {
                    doctorsFromAPI = doctorsFromAPI.Where(p => p.DepartmentName.ToUpper() == Dept.ToUpper()).ToList();
                }
               
                List<Doctor> doctorImages = repository.GetAll();

                foreach (Doctor item in doctorImages)
                {
                    EliteHospital.Data.APIRequestResponseModels.Response.Doctor doctorToUpdate = doctorsFromAPI.Where(p => p.DoctorId.Trim().ToUpper() == item.DoctorId.Trim().ToUpper()).FirstOrDefault();
                    if (doctorToUpdate != null)
                    {
                        doctorToUpdate.Photo = item.DoctorImage;
                        //doctorToUpdate.PositionRole = item.Position;
                        doctorToUpdate.Status = item.Status;
                        doctorToUpdate.Description1 = item.Description1;
                        doctorToUpdate.Description2 = item.Description2;
                        doctorToUpdate.Description3 = item.Description3;
                        doctorToUpdate.DoctorBioStatus = item.DoctorBioStatus;
                    }
                }
                doctorsFromAPI = doctorsFromAPI.Where(p => p.Status == "Y" || p.Status == null).ToList();
                return doctorsFromAPI;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<Department> GetDepartments()
        {
            try
            {
                HMSAPIRespository hmsAPIRespository = new HMSAPIRespository();
                CMSDepartmentRepository departmentRepository = new CMSDepartmentRepository();

                List<EliteHospital.Data.APIRequestResponseModels.Response.Department> departmentsFromAPI = hmsAPIRespository.GetAllDepartments();
                List<Department> apiDepartments = departmentsFromAPI.Select(p => new Department()
                {
                    DepartmentName = p.DepartmentName,
                    Status = p.Status
                }).ToList();
                List<Department> departmentImages = departmentRepository.GetAll();

                foreach (Department item in departmentImages)
                {
                    Department departmentToUpdate = apiDepartments.Where(p => p.DepartmentName.Trim().ToUpper() == item.DepartmentName.Trim().ToUpper()).FirstOrDefault();
                    if (departmentToUpdate != null)
                    {
                        departmentToUpdate.Description = item.Description;
                        departmentToUpdate.LongDescription = item.LongDescription;
                        departmentToUpdate.DepartmentImage = item.DepartmentImage;
                        departmentToUpdate.DepartmentIconImage = item.DepartmentIconImage;
                        departmentToUpdate.Status = item.Status;
                        departmentToUpdate.DepartmentNameArabic = item.DepartmentNameArabic;
                        departmentToUpdate.DescriptionArabic = item.DescriptionArabic;
                    }
                }
                apiDepartments = apiDepartments.Where(p => p.Status == "Y" || p.Status == null).ToList();
                return apiDepartments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}