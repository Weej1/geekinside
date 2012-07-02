using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;

namespace DAL
{
    public class DALEmployeeDetail : IDALEmployeeDetail
    {
        private UserEmployeeModel ConvertFromDB(UserEmployeeDetail dbUser)
        {
            if (dbUser == null) return null;
            return new UserEmployeeModel
            {
                Id = dbUser.Id,
                EmployeeNumber = dbUser.EmployeeNumber,
                Name = dbUser.Name,
                Email = dbUser.Email,
                Phone = dbUser.Phone,
            };
        }

        private UserEmployeeModel ConvertFromDB(UserEmployee dbUser , UserEmployeeDetail dbUserDetail)
        {
            if (dbUser == null || dbUserDetail==null) return null;
            return new UserEmployeeModel
            {
                Id = dbUser.Id,
                EmployeeNumber = dbUser.EmployeeNumber,
                Name = dbUserDetail.Name,
                Email = dbUserDetail.Email,
                Phone = dbUserDetail.Phone,
                IsAvailable = dbUser.IsAvailable,
                IsChecker = dbUser.IsChecker,
                IsManager = dbUser.IsManager,
                DepartmentId = dbUser.DepartmentId,
                LastLoginTime = dbUser.LastLoginTime
            };
        }

        public UserEmployeeModel GetUserEmployeeDetail(int employeeNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                UserEmployeeDetail dbUser = (from u in gikms.UserEmployeeDetails
                                       where u.EmployeeNumber.Equals(employeeNumber)
                                       select u).FirstOrDefault();
                return ConvertFromDB(dbUser);
            }
        }

        public UserEmployeeModel GetSingleEmployeeDetailById(int id) 
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                UserEmployeeDetail dbUserDetail = (from u in gikms.UserEmployeeDetails
                                             where u.Id.Equals(id)
                                             select u).FirstOrDefault();

                UserEmployee dbUser = (from u in gikms.UserEmployees
                                             where u.Id.Equals(id)
                                             select u).FirstOrDefault();

                return ConvertFromDB(dbUser , dbUserDetail);
            }
        }

        public UserEmployeeModel GetSingleEmployeeDetail(int employeeNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                UserEmployeeDetail dbUserDetail = (from u in gikms.UserEmployeeDetails
                                                   where u.EmployeeNumber.Equals(employeeNumber)
                                                   select u).FirstOrDefault();

                UserEmployee dbUser = (from u in gikms.UserEmployees
                                       where u.EmployeeNumber.Equals(employeeNumber)
                                       select u).FirstOrDefault();

                return ConvertFromDB(dbUser, dbUserDetail);
            }
        }

        public List<UserEmployeeModel> GetAllEmployeeDetails() 
        {
            List<UserEmployeeModel> userEmpDetails = new List<UserEmployeeModel>();
            using (var gikms = new geekinsidekmsEntities()) 
            {
                var emps = from d in gikms.UserEmployees
                                 select d;
                if (emps.Count() != 0) 
                {
                    foreach (UserEmployee temp in emps) 
                    {
                        userEmpDetails.Add(GetSingleEmployeeDetail(temp.EmployeeNumber));
                    }
                }
                return userEmpDetails;
            }            
        }

        public List<UserEmployeeModel> GetEmployeeDetailsByDept(int deptId) 
        {
            List<UserEmployeeModel> userEmpDetails = new List<UserEmployeeModel>();
            using (var gikms = new geekinsidekmsEntities())
            {
                var emps = from d in gikms.UserEmployees
                           where d.DepartmentId == deptId
                           select d;
                if (emps.Count() != 0)
                {
                    foreach (UserEmployee temp in emps)
                    {
                        userEmpDetails.Add(GetSingleEmployeeDetail(temp.EmployeeNumber));
                    }
                }
                return userEmpDetails;
            }         
        }
    }
}
