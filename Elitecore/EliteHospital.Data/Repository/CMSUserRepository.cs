using EliteHospital.Core;
using EliteHospital.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSUserRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSUserRepository()
        {
            context = new EliteHospitalEntities();
        }

        public CMSUserViewModel GetUserByCredential(string userName, string password)
        {
            return context.Users.Where(p => p.UserName == userName && p.Password == password).Select(p => new CMSUserViewModel()
            {
                Id = p.ID,
                Name = p.Name,
                UserName = p.UserName,
                Status = p.Status
            }).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            try
            {
                return context.Users.Where(p=>p.Name == "User").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetById(int Id)
        {
            try
            {
                return context.Users.Where(p => p.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(User userdetails)
        {
            try
            {
                context.Users.Add(userdetails);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Update(User userdetails)
        {
            try
            {
                User userdetailsToUpdate = context.Users.Where(p => p.ID == userdetails.ID).FirstOrDefault();
                if (userdetailsToUpdate != null)
                {
                    userdetailsToUpdate.UserName = userdetails.UserName;
                    userdetailsToUpdate.Password = userdetails.Password;
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
                User userdetails = context.Users.Where(p => p.ID == Id).FirstOrDefault();
                if (userdetails != null)
                {
                    context.Users.Remove(userdetails);
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
