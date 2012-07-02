using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALUserAccount
    {
        UserEmployeeModel getUserByEmployeeNumber(int employeeNumber);

        Boolean CreateUserAccount(UserEmployeeModel userEmployeeModel);

        Boolean UpdateUserAccount(UserEmployeeModel userEmployeeModel);

        Boolean DeleteUserAccount(UserEmployeeModel userEmployeeModel, UserEmployeeDetailModel userEmployeeDetailModel);

        UserEmployeeDetailModel GetEmployeeDetailByEmployeeNumber(int employeeNumber);

        int GetMaxEmployeeNumber();

        Boolean UpdateUserDetailAccount(UserEmployeeDetailModel empDetailModel);
    }
}
