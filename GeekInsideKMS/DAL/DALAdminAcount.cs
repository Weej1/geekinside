using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace DAL
{
    public class DALAdminAcount:IDALAdminAcount
    {
        public UserAdminModel getUserByUsername(string username)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var userAdmin = from u in gikms.UserAdmins
                                where u.Username.Equals(username)
                                select u;

                UserAdminModel result = new UserAdminModel();
                result.Username = userAdmin.First().Username;
                result.Password = userAdmin.First().Password;
                return result;
            }
        }
    }
}
