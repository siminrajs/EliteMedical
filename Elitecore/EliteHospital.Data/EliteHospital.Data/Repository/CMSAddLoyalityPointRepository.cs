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
    public class CMSAddLoyalityPointRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSAddLoyalityPointRepository()
        {
            context = new EliteHospitalEntities();
        }
        public List<tbl_PatientMaster> GetAll()
        {
            try
            {
                return context.tbl_PatientMaster.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbl_TransactionHistory> Gettransactiondetails(int Id)
        {
            try
            {
                return context.tbl_TransactionHistory.Where(p => p.TH_Status != "RedeemPending").ToList();
                // return context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tbl_PatientMaster Getpatientdetails(int Id)
        {
            try
            {
                return context.tbl_PatientMaster.Where(p => p.PM_Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tbl_CommonParameter Getcommondetails()
        {
            try
            {
                return context.tbl_CommonParameter.Where(p => p.CP_Param == "ConversionPercentage").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_CommonParameter Addcommondetails()
        {
            try
            {
                return context.tbl_CommonParameter.Where(p => p.CP_Param == "AddLoyaltyPoint").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_CommonParameter GetLoyalityPoint()
        {
            try
            {
                return context.tbl_CommonParameter.Where(p => p.CP_Param == "AddLoyaltyPoint").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbl_TransactionHistory> GetTransactionAll(int Patientid=0)
        {
            try
            {
                // tbl_TransactionHistory dt = new tbl_TransactionHistory();
                //decimal sum =context.tbl_TransactionHistory.Sum(p => p.TH_QtrAmount ?? 0);
                ///double sum = tbl_TransactionHistory.sum(t => t.Amount ?? 0).Sum(); .Select(t => Convert.ToInt32(t.Count)).Sum();
               
                return context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == Patientid && p.TH_Status != "RedeemPending").ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbl_TransactionHistory> GetTAll()
        {
            try
            {
                return context.tbl_TransactionHistory.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_TransactionHistory GetPointBal(int patienid)
        {
            try
            {
                tbl_TransactionHistory dt = new tbl_TransactionHistory();
                tbl_TransactionHistory obj = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == patienid).FirstOrDefault();
                if (obj != null)
                {

                    dt.TH_BalancePoints = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == patienid).Sum(p => p.TH_BalancePoints ?? 0);
                    return dt;
                }
                else
                {
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void saveaddpoints(tbl_TransactionHistory TransactionHistory)
        {
            try
            {
                context.tbl_TransactionHistory.Add(TransactionHistory);
                context.SaveChanges();
                //Update Point Balance
                tbl_TransactionHistory trans = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id && p.TH_Status == "RedeemApproval").FirstOrDefault();

                if (trans == null)
                {
                    decimal addpointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).Sum(p => p.TH_LoyaltyPoints ?? 0);

                    tbl_PatientMaster userdetails = context.tbl_PatientMaster.Where(p => p.PM_Id == TransactionHistory.TH_PM_Id).FirstOrDefault();
                    userdetails.PM_Price = addpointbal;
                    context.SaveChanges();
                }
                else { 
                decimal addpointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).Sum(p => p.TH_LoyaltyPoints ?? 0);
                decimal redeempointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id && p.TH_Status == "RedeemApproval").Sum(p => p.TH_RedeemPoints ?? 0);
                tbl_PatientMaster user = context.tbl_PatientMaster.Where(p => p.PM_Id == TransactionHistory.TH_PM_Id).FirstOrDefault();
                user.PM_Price = addpointbal - redeempointbal;
                context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void redeempoints(tbl_TransactionHistory TransactionHistory)
        {
            try
            {
                tbl_TransactionHistory transdetails = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).FirstOrDefault();
                context.tbl_TransactionHistory.Add(TransactionHistory);
                context.SaveChanges();

                //Update Point Balance
                decimal addpointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).Sum(p => p.TH_LoyaltyPoints ?? 0);
                decimal redeempointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id && p.TH_Status== "RedeemPending").Sum(p => p.TH_RedeemPoints ?? 0);
                tbl_PatientMaster user = context.tbl_PatientMaster.Where(p => p.PM_Id == TransactionHistory.TH_PM_Id).FirstOrDefault();
                user.PM_Price = addpointbal - redeempointbal;
                context.SaveChanges();
                //decimal balpointsum = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).Sum(p => p.TH_BalancePoints ?? 0);
                //decimal redeem = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).Sum(p => p.TH_RedeemPoints ?? 0);
                //decimal RedeemBAl = balpointsum - redeem;

                //decimal balpoint = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id).Sum(p => p.TH_RedeemPoints ?? 0);
                //tbl_TransactionHistory user = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == TransactionHistory.TH_PM_Id && p.TH_Id == TransactionHistory.TH_Id).FirstOrDefault();
                //user.TH_BalancePoints = -balpoint;
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void saveloyaltypoints(tbl_CommonParameter commonParameter)
        {
            try
            {
                tbl_CommonParameter result = context.tbl_CommonParameter.Where(p => p.CP_Param == commonParameter.CP_Param).FirstOrDefault();
                if (result == null)
                {
                    context.tbl_CommonParameter.Add(commonParameter);
                    context.SaveChanges();
                }
                else
                {
                    tbl_CommonParameter updateresult = context.tbl_CommonParameter.Where(p => p.CP_Param == commonParameter.CP_Param).FirstOrDefault();
                    result.CP_Value = commonParameter.CP_Value;
                    context.SaveChanges();
                }       
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbl_CommonParameter> GetCommonParamlist()
        {
            try
            {
                return context.tbl_CommonParameter.ToList();   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void saveservicemaster(tbl_ServiceMaster servicedetails)
        {
            tbl_ServiceMaster result = context.tbl_ServiceMaster.Where(p => p.SM_Id == servicedetails.SM_Id).FirstOrDefault();

            if (result == null)
            {
                context.tbl_ServiceMaster.Add(servicedetails);
                context.SaveChanges();
            }
            else
            {
                tbl_ServiceMaster updateresult = context.tbl_ServiceMaster.Where(p => p.SM_Id == servicedetails.SM_Id).FirstOrDefault();
                updateresult.SM_Name = servicedetails.SM_Name;
                updateresult.SM_Points = servicedetails.SM_Points;
                context.SaveChanges();
            }
            
        }

        public List<tbl_ServiceMaster> ServiceListPartial()
        {
            try
            {
                return context.tbl_ServiceMaster.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteService(int Id)
        {
            try
            {
                tbl_ServiceMaster Service = context.tbl_ServiceMaster.Where(p => p.SM_Id == Id).FirstOrDefault();
                if (Service != null)
                {
                    context.tbl_ServiceMaster.Remove(Service);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_ServiceMaster Getservicedetails(int Id)
        {
            try
            {
                return context.tbl_ServiceMaster.Where(p => p.SM_Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_TransactionHistory Checkpromocode(string promocode,int patientid)
        {
            try
            {
                return context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == patientid && p.TH_Promocode==promocode&&p.TH_Status!= "RedeemApproval").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
       public void Updateredeempoints(tbl_TransactionHistory TransactionHistory)
        {
            try
            {
               // tbl_PatientMaster patientdetails = context.tbl_PatientMaster.Where(p => p. == TransactionHistory.TH_PM_Id).FirstOrDefault();
                tbl_TransactionHistory trandetails = context.tbl_TransactionHistory.Where(p => p.TH_Promocode == TransactionHistory.TH_Promocode &&p.TH_PM_Id==TransactionHistory.TH_PM_Id).FirstOrDefault();
                trandetails.TH_Status = TransactionHistory.TH_Status;
                context.SaveChanges();

                //Update Redeem Balance
                decimal addpointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == trandetails.TH_PM_Id).Sum(p => p.TH_LoyaltyPoints ?? 0);
                decimal redeempointbal = context.tbl_TransactionHistory.Where(p => p.TH_PM_Id == trandetails.TH_PM_Id&&p.TH_Status!= "RedeemPending").Sum(p => p.TH_RedeemPoints ?? 0);
                tbl_PatientMaster user = context.tbl_PatientMaster.Where(p => p.PM_Id == TransactionHistory.TH_PM_Id).FirstOrDefault();
                user.PM_Price = addpointbal - redeempointbal;
                context.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



