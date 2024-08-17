using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSSpecialOfferRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSSpecialOfferRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<SpecialOffer> GetAll()
        {
            try
            {
                return context.SpecialOffers.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SpecialOffer GetById(int Id)
        {
            try
            {
                return context.SpecialOffers.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(SpecialOffer specialOffer)
        {
            try
            {
                context.SpecialOffers.Add(specialOffer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(SpecialOffer specialOffer)
        {
            try
            {
                SpecialOffer specialOfferToUpdate = context.SpecialOffers.Where(p => p.Id == specialOffer.Id).FirstOrDefault();
                if (specialOfferToUpdate != null)
                {
                    if (specialOffer.Image != null && specialOffer.Image.Length > 0)
                    {
                        specialOfferToUpdate.Image = specialOffer.Image;
                        specialOfferToUpdate.ImagePath = specialOffer.ImagePath;
                    }
                    if (specialOffer.ImageMob != null && specialOffer.ImageMob.Length > 0)
                    {
                        specialOfferToUpdate.ImageMob = specialOffer.ImageMob;
                        specialOfferToUpdate.ImageMobPath = specialOffer.ImageMobPath;
                    }
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
                SpecialOffer specialOffer = context.SpecialOffers.Where(p => p.Id == Id).FirstOrDefault();
                if (specialOffer != null)
                {
                    context.SpecialOffers.Remove(specialOffer);
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
