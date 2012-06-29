using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using DAL;
using IDAL;

namespace BLL
{
    public class BLLAdminAcount
    {
        public Boolean CheckAdminLogin(UserAdminModel userAdminModel)
        {
            IDALAdminAcount dalAdminAccount = new DALAdminAcount();
            UserAdminModel userAdminModelResult = dalAdminAccount.getUserByUsername(userAdminModel.Username);
            if (userAdminModel.Username == userAdminModelResult.Username && userAdminModel.Password == userAdminModelResult.Password)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
