using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLUserAccount
    {
        IDALUserAccount userDAL = DALFactory.DataAccess.CreateUserDAL();

        public Boolean CheckUserLogin(UserEmployeeModel userEmployeeModel)
        {
            UserEmployeeModel user = userDAL.getUserByEmployeeNumber(userEmployeeModel.EmployeeNumber);
            if (user != null && user.Password == userEmployeeModel.Password)
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
