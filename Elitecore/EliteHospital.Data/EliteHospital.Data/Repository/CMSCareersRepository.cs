using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSCareersRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSCareersRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<Career> GetAll()
        {
            try
            {
                return context.Careers.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Career GetById(int Id)
        {
            try
            {
                return context.Careers.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Career career)
        {
            try
            {
                context.Careers.Add(career);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Career career)
        {
            try
            {
                Career careerToUpdate = context.Careers.Where(p => p.Id == career.Id).FirstOrDefault();
                if (careerToUpdate != null)
                {
                                            careerToUpdate.Vacancy = career.Vacancy;
                        careerToUpdate.PositionDescription = career.PositionDescription;
                    careerToUpdate.Email = career.Email;
                }
                context.SaveChanges();
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
                Career career = context.Careers.Where(p => p.Id == Id).FirstOrDefault();
                if (career != null)
                {
                    context.Careers.Remove(career);
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
