using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;

namespace EliteHospital.Data.Repository
{
    public class CMSMediaRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSMediaRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<tbl_Media> GetAll()
        {
            try
            {
                List<tbl_Media> aa=context.tbl_Media.ToList();
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (tbl_Media drow in aa)
                {
                    if (hTable.Contains(drow.Category))
                        duplicateList.Add(drow);
                    else
                        hTable.Add(drow.Category, string.Empty);
                }

                foreach (tbl_Media dRow in duplicateList)
                    aa.Remove(dRow);
                return aa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_Media GetById(int Id)
        {
            try
            {
                return context.tbl_Media.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbl_MediaImagesList> GetMediaImagelist(int Id)
        {
            try
            {
                return context.tbl_MediaImagesList.Where(p => p.MediaId == Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_Media GetCatDetails(string category)
        {
            try
            {
                return context.tbl_Media.Where(p => p.Category == category).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbl_Media> Getlist(int Id)
        {
            try
            {
                return context.tbl_Media.Where(p => p.Id == Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbl_MediaImagesList> GetImagelist()
        {
            try
            {
                return context.tbl_MediaImagesList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbl_MediaImagesList> GetImage(int id)
        {
            try
            {
                return context.tbl_MediaImagesList.Where(p => p.MediaId == id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(tbl_Media covidbanner)
        {
            try
            {
                context.tbl_Media.Add(covidbanner);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tbl_Media covidbanner)
        {
            try
            {
                tbl_Media covidbannerToUpdate = context.tbl_Media.Where(p => p.Id == covidbanner.Id).FirstOrDefault();
                if (covidbannerToUpdate != null)
                {
                    covidbannerToUpdate.Category = covidbanner.Category;
                    covidbannerToUpdate.Title = covidbanner.Title;
                    covidbannerToUpdate.Category_Arabic = covidbanner.Category_Arabic;
                    //if (covidbanner.Image != null && covidbanner.Image.Length > 0)
                    //{
                    //    covidbannerToUpdate.Image = covidbanner.Image;
                    //}
                   
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
                tbl_Media covidbanner = context.tbl_Media.Where(p => p.Id == Id).FirstOrDefault();
                if (covidbanner != null)
                {
                    context.tbl_Media.Remove(covidbanner);
                    context.SaveChanges();
                }
                //List<tbl_MediaImagesList> MediaImagesList = context.tbl_MediaImagesList.Where(p => p.MediaId == Id).List();
                //if (MediaImagesList != null)
                //{
                //    context.tbl_MediaImagesList.Remove(MediaImagesList);
                //    context.SaveChanges();

                //}
                List<tbl_MediaImagesList> MediaImagesList = context.tbl_MediaImagesList.Where(p => p.MediaId == Id).ToList();
                if (MediaImagesList != null)
                {
                    foreach (var listItem in MediaImagesList)
                    {
                        context.tbl_MediaImagesList.Remove(listItem);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteImagebyid(int Id)
        {
            try
            {
                tbl_MediaImagesList covidbanner = context.tbl_MediaImagesList.Where(p => p.Id == Id).FirstOrDefault();
                if (covidbanner != null)
                {
                    context.tbl_MediaImagesList.Remove(covidbanner);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddMediaImage(tbl_MediaImagesList _media)
        {
            try
            {
                tbl_MediaImagesList data = context.tbl_MediaImagesList.Add(_media);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
