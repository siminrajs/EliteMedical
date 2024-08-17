
using EliteHospital.Core;
using EliteHospital.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSTermsAndConRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSTermsAndConRepository()
        {
            context = new EliteHospitalEntities();
        }

        public List<tbl_TermsAndConditions_Admin> GetAll()
        {
            try
            {
                return context.tbl_TermsAndConditions_Admin.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public tbl_TermsAndConditions_Admin GetById(int Id)
        {
            try
            {
                return context.tbl_TermsAndConditions_Admin.Where(p => p.TC_Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(tbl_TermsAndConditions_Admin termsAndCon)
        {
            try
            {
                context.tbl_TermsAndConditions_Admin.Add(termsAndCon);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tbl_TermsAndConditions_Admin career)
        {
            try
            {
                tbl_TermsAndConditions_Admin careerToUpdate = context.tbl_TermsAndConditions_Admin.Where(p => p.TC_Id == career.TC_Id).FirstOrDefault();
                if (careerToUpdate != null)
                {
                    careerToUpdate.TC_Description = career.TC_Description;
                    careerToUpdate.TC_Description_Arabic = career.TC_Description_Arabic;
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void Update1(tbl_TermsAndConditions_Admin termsAndCon)
        //{
        //    try
        //    {
        //        tbl_TermsAndConditions_Admin termsAndConup = context.tbl_TermsAndConditions_Admin.Where(p => p.TC_Id == 4).FirstOrDefault();

        //        tbl_TermsAndConditions_Admin termsAndConToUpdate = context.tbl_TermsAndConditions_Admin.Where(p => p.TC_Id == termsAndCon.TC_Id).FirstOrDefault();
        //        if (termsAndConup != null)
        //        {
        //            termsAndConToUpdate.TC_Description = termsAndConToUpdate.TC_Description;
        //            termsAndConToUpdate.TC_Description_Arabic = termsAndConToUpdate.TC_Description_Arabic;
        //        }
        //        context.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void Delete(int Id)
        {
            try
            {
                tbl_TermsAndConditions_Admin termsAndCon = context.tbl_TermsAndConditions_Admin.Where(p => p.TC_Id == Id).FirstOrDefault();
                if (termsAndCon != null)
                {
                    context.tbl_TermsAndConditions_Admin.Remove(termsAndCon);
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
