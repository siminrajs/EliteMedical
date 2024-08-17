using EliteHospital.Data.Repository;
using EliteHospital.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Services
{
    public class CMSService
    {
        private readonly CMSUserRepository cmsUserRepository;
        public CMSService()
        {
            cmsUserRepository = new CMSUserRepository();
        }
        public CMSUserViewModel GetCMSUserByCredential(LoginViewModel loginViewModel)
        {
            return cmsUserRepository.GetUserByCredential(loginViewModel.UserName, loginViewModel.Password);
        }

       
    }
}
