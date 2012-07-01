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
    }
}
