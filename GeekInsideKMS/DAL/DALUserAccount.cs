using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;

namespace DAL
{
    public class DALUserAccount: IDALUserAccount
    {
        private UserEmployeeModel ConvertFromDB(UserEmployee dbUser)
        {
            if (dbUser == null) return null;
            return new UserEmployeeModel
            {
                Id = dbUser.Id,
                EmployeeNumber = dbUser.EmployeeNumber,
                Password = dbUser.Password,
                IsChecker = dbUser.IsChecker,
                IsAvailable = dbUser.IsAvailable,               
                LastLoginTime = dbUser.LastLoginTime
            };
        }

        public UserEmployeeModel getUserByEmployeeNumber(int employeeNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                UserEmployee dbUser = (from u in gikms.UserEmployees
                                       where u.EmployeeNumber.Equals(employeeNumber)
                                       select u).FirstOrDefault();
                return ConvertFromDB(dbUser);
            }
        }
    }
}
