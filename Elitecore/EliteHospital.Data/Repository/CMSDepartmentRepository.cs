using EliteHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.Repository
{
    public class CMSDepartmentRepository
    {
        private readonly EliteHospitalEntities context;
        public CMSDepartmentRepository()
        {
            context = new EliteHospitalEntities();
            this.context.Database.CommandTimeout = int.MaxValue;
        }

        public List<Department> GetAll()
        {
            try
            {
                return context.Departments.Where(p => p.Status == "Y" && p.Department_Type != "Other").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Department> GetAllOthers()
        {
            try
            {
                return context.Departments.Where(p => p.Status == "Y" && p.Department_Type == "Other").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Department> GetAllBooking()
        {
            try
            {
                return context.Departments.Where(p => p.Status == "Y").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Department GetByName(int DepartmentId)
        {
            try
            {
                return context.Departments.Where(p => p.DepartmentId == DepartmentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Department GetByDept(string dept)
        {
            try
            {
                return context.Departments.Where(p => p.DepartmentName == dept).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Department department)
        {
            try
            {
                Department departmentToUpdate = context.Departments.Where(p => p.DepartmentId == department.DepartmentId).FirstOrDefault();
                if (departmentToUpdate != null)
                {
                    departmentToUpdate.DepartmentName = department.DepartmentName;
                    departmentToUpdate.DepartmentNameArabic = department.DepartmentNameArabic;
                    departmentToUpdate.Description = department.Description;
                    departmentToUpdate.LongDescription = department.LongDescription;
                    departmentToUpdate.DescriptionArabic = department.DescriptionArabic;
                    departmentToUpdate.Status = department.Status;
                    departmentToUpdate.OrderNo = department.OrderNo;
                    departmentToUpdate.Department_Image = department.Department_Image;
                    departmentToUpdate.Department_Icon = department.Department_Icon;
                    departmentToUpdate.Department_Type = department.Department_Type;
                    //if ((department.DepartmentImage != null && department.DepartmentImage.Length > 0) || 
                    //    string.IsNullOrEmpty(department.DepartmentImagePath))
                    //{
                    //    departmentToUpdate.DepartmentImage = department.DepartmentImage;
                    //    departmentToUpdate.DepartmentImagePath = department.DepartmentImagePath;
                    //}
                    //if ((department.DepartmentImageMob != null && department.DepartmentImageMob.Length > 0) ||
                    //        string.IsNullOrEmpty(department.DepartmentImageMobPath))
                    //{
                    //    departmentToUpdate.DepartmentImageMob = department.DepartmentImageMob;
                    //    departmentToUpdate.DepartmentImageMobPath = department.DepartmentImageMobPath;
                    //}
                    //if ((department.DepartmentIconImage != null && department.DepartmentIconImage.Length > 0) ||
                    //    string.IsNullOrEmpty(department.DepartmentIconImagePath))
                    //{
                    //    departmentToUpdate.DepartmentIconImage = department.DepartmentIconImage;
                    //    departmentToUpdate.DepartmentIconImagePath = department.DepartmentIconImagePath;
                    //}
                    context.SaveChanges();
                }
                else
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Delete(int DepartmentId)
        {
            try
            {
                Department department = context.Departments.Where(p => p.DepartmentId == DepartmentId).FirstOrDefault();
                if (department != null)
                {
                    context.Departments.Remove(department);
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
