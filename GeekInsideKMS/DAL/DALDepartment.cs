using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;
using System.Data;

namespace DAL
{
    public class DALDepartment : IDALDepartment
    {
        private DepartmentModel ConvertFromDB(Department dbDepartment)
        {
            return new DepartmentModel
            {
                Id = dbDepartment.Id,
                DepartmentName = dbDepartment.DepartmentName,
                FolderId = dbDepartment.FolderId
            };
        }

        public int CreateDepartment(DepartmentModel department)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                Department dbDepartment = new Department
                {
                    DepartmentName = department.DepartmentName,
                    FolderId = department.FolderId
                };
                context.Departments.AddObject(dbDepartment);
                context.SaveChanges();
                return dbDepartment.Id;
            }
        }

        public void UpdateDepartment(DepartmentModel department)
        {
            using (geekinsidekmsEntities context =
               new geekinsidekmsEntities())
            {
                Department dbDepartment = new Department
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    FolderId = department.FolderId
                };
                context.Departments.AddObject(dbDepartment);
                context.ObjectStateManager.ChangeObjectState(dbDepartment, EntityState.Modified);
                context.SaveChanges();
            }
        }

        public void DeleteDepartment(DepartmentModel department)
        {
            using (geekinsidekmsEntities context =
               new geekinsidekmsEntities())
            {
                Department dbDept = (from d in context.Departments
                                     where d.Id == department.Id
                                     select d).FirstOrDefault();
                if (dbDept != null)
                {
                    context.Departments.DeleteObject(dbDept);
                    context.SaveChanges();
                }
            }
        }

        public IList<DepartmentModel> GetAllDepartments()
        {
            IList<DepartmentModel> result = new List<DepartmentModel>();
            using (geekinsidekmsEntities context =
               new geekinsidekmsEntities())
            {
                var list = from d in context.Departments select d;
                foreach (Department dbDept in list)
                {
                    result.Add(ConvertFromDB(dbDept));
                }
                return result;
            }
        }

        public DepartmentModel GetDepartmentById(int dept_id)
        {
            using (geekinsidekmsEntities context =
               new geekinsidekmsEntities())
            {
                Department dbDepartment = (from d in context.Departments
                                           where d.Id == dept_id
                                           select d).FirstOrDefault();
                if (dbDepartment == null) return null;
                return ConvertFromDB(dbDepartment);
            }
        }
    }
}
