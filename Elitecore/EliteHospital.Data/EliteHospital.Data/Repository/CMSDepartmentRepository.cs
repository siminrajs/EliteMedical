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
                return context.Departments.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Department GetByName(string Name)
        {
            try
            {
                return context.Departments.Where(p => p.DepartmentName == Name).FirstOrDefault();
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
                Department departmentToUpdate = context.Departments.Where(p => p.DepartmentName == department.DepartmentName).FirstOrDefault();
                if (departmentToUpdate != null)
                {
                    departmentToUpdate.DepartmentName = department.DepartmentName;
                    departmentToUpdate.DepartmentNameArabic = department.DepartmentNameArabic;
                    departmentToUpdate.Description = department.Description;
                    departmentToUpdate.LongDescription = department.LongDescription;
                    departmentToUpdate.DescriptionArabic = department.DescriptionArabic;
                    departmentToUpdate.Status = department.Status;
                    departmentToUpdate.OrderNo = department.OrderNo;
                    if ((department.DepartmentImage != null && department.DepartmentImage.Length > 0) || 
                        string.IsNullOrEmpty(department.DepartmentImagePath))
                    {
                        departmentToUpdate.DepartmentImage = department.DepartmentImage;
                        departmentToUpdate.DepartmentImagePath = department.DepartmentImagePath;
                    }
                    if ((department.DepartmentImageMob != null && department.DepartmentImageMob.Length > 0) ||
                            string.IsNullOrEmpty(department.DepartmentImageMobPath))
                    {
                        departmentToUpdate.DepartmentImageMob = department.DepartmentImageMob;
                        departmentToUpdate.DepartmentImageMobPath = department.DepartmentImageMobPath;
                    }
                    if ((department.DepartmentIconImage != null && department.DepartmentIconImage.Length > 0) ||
                        string.IsNullOrEmpty(department.DepartmentIconImagePath))
                    {
                        departmentToUpdate.DepartmentIconImage = department.DepartmentIconImage;
                        departmentToUpdate.DepartmentIconImagePath = department.DepartmentIconImagePath;
                    }
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


        public void Delete(string Name)
        {
            try
            {
                Department department = context.Departments.Where(p => p.DepartmentName == Name).FirstOrDefault();
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
