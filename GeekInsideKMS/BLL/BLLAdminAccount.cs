using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;
using Utils;

namespace BLL
{
    public class BLLAdminAcount
    {
        IDALAdminAccount adminDAL = DALFactory.DataAccess.CreateAdminDAL();

        public Boolean CheckAdminLogin(UserAdminModel userAdminModel)
        {
            UserAdminModel admin = adminDAL.getUserByUsername(userAdminModel.Username);
            if (admin != null && this.CheckPassword(admin,userAdminModel.Password))
            {
                return true;
            }
            else {
                return false;
            }
        }

        public Boolean CheckPassword(UserAdminModel userAdminModel, string password) 
        {
            string encryptPassword = Helper.EncryptByMD5(password);
            return userAdminModel.Password == encryptPassword;
        }
    }
}
