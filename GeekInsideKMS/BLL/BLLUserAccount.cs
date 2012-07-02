﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;
using Utils;

namespace BLL
{
    public class BLLUserAccount
    {
        IDALUserAccount userDAL = DALFactory.DataAccess.CreateUserDAL();

        public Boolean CheckUserLogin(UserEmployeeModel userEmployeeModel)
        {
            UserEmployeeModel user = userDAL.getUserByEmployeeNumber(userEmployeeModel.EmployeeNumber);
            if (user != null && this.CheckPassword(user, userEmployeeModel.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CheckPassword(UserEmployeeModel userEmployeeModel, string password)
        {
            string encryptPassword = Helper.EncryptByMD5(password);
            return userEmployeeModel.Password == encryptPassword;
        }

        public Boolean CreateUserAccount(UserEmployeeModel userEmployeeModel, UserEmployeeDetailModel userEmployeeDetailModel)
        {
            userEmployeeModel.Password = Helper.EncryptByMD5(userEmployeeModel.Password);
            userDAL.CreateUserAccount(userEmployeeModel, userEmployeeDetailModel);
            return true;
        }

        public Boolean DeleteUserAccount(UserEmployeeModel userEmployeeModel, UserEmployeeDetailModel userEmployeeDetailModel)
        {
            userDAL.DeleteUserAccount(userEmployeeModel, userEmployeeDetailModel);
            return true;
        }

        public Boolean UpdateUserAccount(UserEmployeeModel userEmployeeModel, UserEmployeeDetailModel userEmployeeDetailModel)
        {
            userDAL.UpdateUserAccount(userEmployeeModel, userEmployeeDetailModel);
            return true;
        }

        public UserEmployeeDetailModel GetUserDetailByEmpNumber(int empNumber)
        {
            return userDAL.GetEmployeeDetailByEmployeeNumber(empNumber);
        }

        public UserEmployeeModel GetUserByEmpNumber(int empNumber)
        {
            return userDAL.getUserByEmployeeNumber(empNumber);
        }

        public int GetMaxEmployeeNumber()
        {
            return userDAL.GetMaxEmployeeNumber();
        }

        public Boolean UpdateUserDetailAccount(UserEmployeeDetailModel empDetailModel)
        {
            return userDAL.UpdateUserDetailAccount(empDetailModel);
        }
    }
}
