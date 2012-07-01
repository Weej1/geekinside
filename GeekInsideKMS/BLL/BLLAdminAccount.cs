using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLAdminAcount
    {
        IDALAdminAccount adminDAL = DALFactory.DataAccess.CreateAdminDAL();

        public Boolean CheckAdminLogin(UserAdminModel userAdminModel)
        {
            UserAdminModel admin = adminDAL.getUserByUsername(userAdminModel.Username);
            if (admin != null && admin.Password == userAdminModel.Password)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
