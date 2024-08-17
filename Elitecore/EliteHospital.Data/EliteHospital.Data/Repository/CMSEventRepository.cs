using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSEventRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSEventRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<Event> GetAll()
        {
            try
            {
                return context.Events.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Event GetById(int Id)
        {
            try
            {
                return context.Events.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Event _event)
        {
            try
            {
                Event item = context.Events.Add(_event);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Event _event)
        {
            try
            {
                Event eventToUpdate = context.Events.Where(p => p.Id == _event.Id).FirstOrDefault();
                if (eventToUpdate != null)
                {
                    eventToUpdate.Title = _event.Title;
                    eventToUpdate.ShortDescription = _event.ShortDescription;
                    eventToUpdate.Description = _event.Description;
                    if (_event.Image != null && _event.Image.Length > 0)
                    {
                        eventToUpdate.Image = _event.Image;
                        eventToUpdate.ImagePath = _event.ImagePath;
                    }
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddEventImage(EventImage eventImage)
        {
            try
            {
                EventImage data = context.EventImages.Add(eventImage);
                context.SaveChanges();
                return data.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventImage(int Id)
        {
            try
            {
                EventImage eventImage = context.EventImages.Where(p => p.Id == Id).FirstOrDefault();
                if (eventImage != null)
                {
                    context.EventImages.Remove(eventImage);
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
                Event _event = context.Events.Where(p => p.Id == Id).FirstOrDefault();
                if (_event != null)
                {
                    context.Events.Remove(_event);
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
