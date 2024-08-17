using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSContactusRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSContactusRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<ContactU> GetAll()
        {
            try
            {
                return context.ContactUs.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContactU GetById(int Id)
        {
            try
            {
                return context.ContactUs.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ContactU contactus)
        {
            try
            {
                context.ContactUs.Add(contactus);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(ContactU contactus)
        {
            try
            {
                ContactU contactusToUpdate = context.ContactUs.Where(p => p.Id == contactus.Id).FirstOrDefault();
                if (contactusToUpdate != null)
                {
                    contactusToUpdate.Address = contactus.Address;
                    contactusToUpdate.Email = contactus.Email;
                    contactusToUpdate.Phone = contactus.Phone;
                    contactusToUpdate.WorkingHours = contactus.WorkingHours;
                    contactusToUpdate.Location = contactus.Location;
                    contactusToUpdate.AddressMob = contactus.AddressMob;
                    contactusToUpdate.WorkingHoursMob = contactus.WorkingHoursMob;
                    contactusToUpdate.AddressArabicMob = contactus.AddressArabicMob;
                    contactusToUpdate.WorkingHoursArabicMob = contactus.WorkingHoursArabicMob;
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
                ContactU contact = context.ContactUs.Where(p => p.Id == Id).FirstOrDefault();
                if (contact != null)
                {
                    context.ContactUs.Remove(contact);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void SaveContactDetails(tbl_Contact contactdetails)
        {
            try
            {
                context.tbl_Contact.Add(contactdetails);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
