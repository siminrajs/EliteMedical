using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSDailyOffersRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSDailyOffersRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<DailyOffer> GetAll()
        {
            try
            {
                return context.DailyOffers.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DailyOffer GetById(int Id)
        {
            try
            {
                return context.DailyOffers.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(DailyOffer dailyOffer)
        {
            try
            {
                DailyOffer dailyOfferToUpdate = context.DailyOffers.Where(p => p.Id == dailyOffer.Id).FirstOrDefault();
                if (dailyOfferToUpdate != null)
                {
                    if (dailyOffer.Image != null && dailyOffer.Image.Length > 0)
                    {
                        dailyOfferToUpdate.Image = dailyOffer.Image;
                        dailyOfferToUpdate.ImagePath = dailyOffer.ImagePath;
                    }
                    if (dailyOffer.ImageMob != null && dailyOffer.ImageMob.Length > 0)
                    {
                        dailyOfferToUpdate.ImageMob = dailyOffer.ImageMob;
                        dailyOfferToUpdate.ImageMobPath = dailyOffer.ImageMobPath;
                    }
                    context.SaveChanges();
                }
                else
                {
                    context.DailyOffers.Add(dailyOffer);
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
                DailyOffer dailyOffer = context.DailyOffers.Where(p => p.Id == Id).FirstOrDefault();
                if (dailyOffer != null)
                {
                    context.DailyOffers.Remove(dailyOffer);
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
