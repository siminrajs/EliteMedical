using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSDoctorRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSDoctorRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<Doctor> GetAll()
        {
            try
            {
                return context.Doctors.OrderBy(x => x.OrderNo).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Doctor GetById(int Id)
        {
            try
            {
                return context.Doctors.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Doctor doctor)
        {
            try
            {
                Doctor doctorToUpdate = context.Doctors.Where(p => p.Id == doctor.Id).FirstOrDefault();
                if (doctorToUpdate != null)
                {
                    doctorToUpdate.DoctorId = doctor.DoctorId;
                    doctorToUpdate.DoctorName = doctor.DoctorName;
                    doctorToUpdate.DoctorImagePath = doctor.DoctorImagePath;
                    doctorToUpdate.DoctorImageMobPath = doctor.DoctorImageMobPath;
                    doctorToUpdate.OrderNo = doctor.OrderNo;
                    doctorToUpdate.Position = doctor.Position;
                    doctorToUpdate.Status = doctor.Status;
                    doctorToUpdate.DepartmentName = doctor.DepartmentName;
                    doctorToUpdate.DoctorNameArabic = doctor.DoctorNameArabic;
                    doctorToUpdate.Description1 = doctor.Description1;
                    doctorToUpdate.Description2 = doctor.Description2;
                    doctorToUpdate.Description3 = doctor.Description3;
                    doctorToUpdate.DoctorDescription = doctor.DoctorDescription;
                    doctorToUpdate.DoctorDescription_Arabic=doctor.DoctorDescription_Arabic;
                    doctorToUpdate.Doctor_Image = doctor.Doctor_Image;
                    
                    if (doctor.DoctorImage != null && doctor.DoctorImage.Length > 0)
                    {
                        doctorToUpdate.DoctorImage = doctor.DoctorImage;
                    }
                    if (doctor.DoctorImageMob != null && doctor.DoctorImageMob.Length > 0)
                    {
                        doctorToUpdate.DoctorImageMob = doctor.DoctorImageMob;
                    }
                    context.SaveChanges();
                }
                else
                {
                    context.Doctors.Add(doctor);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int Id)
        {
            try
            {
                Doctor doctor = context.Doctors.Where(p => p.Id == Id).FirstOrDefault();
                if (doctor != null)
                {
                    context.Doctors.Remove(doctor);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Doctor GetDoctorDetails(string Doctorid = null)
        {
            try
            {
                return context.Doctors.Where(p => p.DoctorId == Doctorid).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDoctorBIoStatus(int id)
        {
            try
            {
                Doctor doctorDetails = context.Doctors.Where(p => p.Id == id).FirstOrDefault();
                if (doctorDetails.DoctorBioStatus == "ACTIVE")
                {
                    doctorDetails.DoctorBioStatus = "INACTIVE";     
                    context.SaveChanges();
                }
                else
                {
                    doctorDetails.DoctorBioStatus = "ACTIVE";              
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Doctor> GetDeptName(string Dept)
        {
            try
            {
                return context.Doctors.Where(p => p.DepartmentName == Dept).OrderBy(x => x.OrderNo).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
