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
                return context.Doctors.ToList();
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
                    doctorToUpdate.DoctorName = doctor.DoctorName;
                    doctorToUpdate.DoctorImagePath = doctor.DoctorImagePath;
                    doctorToUpdate.DoctorImageMobPath = doctor.DoctorImageMobPath;
                    doctorToUpdate.OrderNo = doctor.OrderNo;
                    doctorToUpdate.Status = doctor.Status;
                    doctorToUpdate.Position = doctor.Position;
                    doctorToUpdate.DoctorNameArabic = doctor.DoctorNameArabic;
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
    }
}
