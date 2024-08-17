using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSOnlineConsultationRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSOnlineConsultationRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<OnlineConsultation> GetAll()
        {
            try
            {
                return context.OnlineConsultations.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OnlineConsultation GetById(int Id)
        {
            try
            {
                return context.OnlineConsultations.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(OnlineConsultation onlineConsultation)
        {
            try
            {
                OnlineConsultation item = context.OnlineConsultations.Add(onlineConsultation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(OnlineConsultation onlineconsultation)
        {
            try
            {
                OnlineConsultation onlineconsulationToUpdate = context.OnlineConsultations.Where(p => p.Id == onlineconsultation.Id).FirstOrDefault();
                if (onlineconsulationToUpdate != null)
                {
                    onlineconsulationToUpdate.Title = onlineconsultation.Title;
                    onlineconsulationToUpdate.Description = onlineconsultation.Description;
                    if (onlineconsultation.Image != null && onlineconsultation.Image.Length > 0)
                    {
                        onlineconsulationToUpdate.Image = onlineconsultation.Image;
                        onlineconsulationToUpdate.Path = onlineconsultation.Path;
                    }
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddVideoUrl(VideoUrl videoUrl)
        {
            try
            {
                VideoUrl data = context.VideoUrls.Add(videoUrl);
                context.SaveChanges();
                return data.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVideoUrl(int Id)
        {
            try
            {
                VideoUrl videoUrl = context.VideoUrls.Where(p => p.Id == Id).FirstOrDefault();
                if (videoUrl != null)
                {
                    context.VideoUrls.Remove(videoUrl);
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
                OnlineConsultation onlineconsulation = context.OnlineConsultations.Where(p => p.Id == Id).FirstOrDefault();
                if (onlineconsulation != null)
                {
                    context.OnlineConsultations.Remove(onlineconsulation);
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
