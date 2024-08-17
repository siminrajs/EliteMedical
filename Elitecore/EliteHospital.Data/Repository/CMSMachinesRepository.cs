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
    public class CMSMachinesRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSMachinesRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<tbl_Machines> GetAll()
        {
            try
            {
                return context.tbl_Machines.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbl_Machines> GetForDownload(int id)
        {
            try
            {
                return context.tbl_Machines.Where(p => p.Id == id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_Machines GetById(int Id)
        {
            try
            {
                return context.tbl_Machines.Where(p => p.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(tbl_Machines covidbanner)
        {
            try
            {
                context.tbl_Machines.Add(covidbanner);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tbl_Machines covidbanner)
        {
            try
            {
                tbl_Machines covidbannerToUpdate = context.tbl_Machines.Where(p => p.Id == covidbanner.Id).FirstOrDefault();
                if (covidbannerToUpdate != null)
                {
                    //covidbannerToUpdate. = covidbanner.Category;
                    if (covidbanner.Image != null && covidbanner.Image.Length > 0)
                    {
                        covidbannerToUpdate.Image = covidbanner.Image;
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
                tbl_Machines covidbanner = context.tbl_Machines.Where(p => p.Id == Id).FirstOrDefault();
                if (covidbanner != null)
                {
                    context.tbl_Machines.Remove(covidbanner);
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
