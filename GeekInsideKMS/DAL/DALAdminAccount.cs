using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace DAL
{
    public class DALAdminAccount:IDALAdminAcount
    {
        private UserAdminModel ConvertFromDB(UserAdmin dbAdmin)
        {
            if (dbAdmin == null) return null;
            return new UserAdminModel
            {
                Id = dbAdmin.Id,
                EmployeeNumber = dbAdmin.EmployeeNumber,
                Username = dbAdmin.Username,
                Password = dbAdmin.Password,
                LastLoginTime = dbAdmin.LastLoginTime
            };
        }

        public UserAdminModel getUserByUsername(string username)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                UserAdmin dbAdmin = (from u in gikms.UserAdmins
                                           where u.Username.Equals(username)
                                           select u).FirstOrDefault();
                return ConvertFromDB(dbAdmin);
            }
        }
    }
}
