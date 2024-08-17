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
    }
}
