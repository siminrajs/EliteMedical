﻿ using EliteHospital.Core;
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

        public List<Event> GetAll(string type)
        {
            try
            {
                return context.Events.Where(p => p.Type == type).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetAllList()
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
        //public List<EventImage> AddEventImagelist(EventImage Imagelist)
        //{
        //    try
        //    {
        //        foreach (EventImage employee in Imagelist)
        //        {
        //          return context.EventImage.Add(employee);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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
        public List<Event> Getdetails(int Id,string type)
        {
            try
            {
                return context.Events.Where(p => p.Id == Id && p.Type==type).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventImagesList> Getimagedetails(int Id)
        {
            try
            {
                return context.EventImagesLists.Where(p => p.EventId == Id).ToList();
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

        public void SaveEventImages(EventImage _event)
        {
            try
            {
                EventImage item = context.EventImages.Add(_event);
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
                    eventToUpdate.Title_Arabic = _event.Title_Arabic;
                    eventToUpdate.Description_Arabic = _event.Description_Arabic;
                    eventToUpdate.CreatedDate = _event.CreatedDate;
                    //eventToUpdate.ShortDescription = _event.ShortDescription;
                    eventToUpdate.Description = _event.Description;
                    //if (_event.Image != null && _event.Image.Length > 0)
                    //{
                    //    eventToUpdate.Image = _event.Image;
                    //    eventToUpdate.ImagePath = _event.ImagePath;
                    //}
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddEventImage(EventImagesList _event)
        {
            try
            {     
                EventImagesList data = context.EventImagesLists.Add(_event);
                context.SaveChanges();
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

        public void DeleteEventImagelist(int Id)
        {
            try
            {
                EventImagesList eventImage = context.EventImagesLists.Where(p => p.Id == Id).FirstOrDefault();
                if (eventImage != null)
                {
                    context.EventImagesLists.Remove(eventImage);
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
                List<EventImagesList> _eventlist = context.EventImagesLists.Where(p => p.EventId == Id).ToList();
                if (_eventlist != null)
                {
                    foreach (var listItem in _eventlist)
                    {
                        context.EventImagesLists.Remove(listItem);
                        context.SaveChanges();
                    }
                }
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EventImagesList> GetEventImagelist(int Id)
        {
            try
            {
                return context.EventImagesLists.Where(p => p.EventId == Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
