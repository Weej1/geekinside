using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALEmployeeDetail
    {
        UserEmployeeModel GetUserEmployeeDetail(int employeeNumber);

        List<UserEmployeeModel> GetAllEmployeeDetails();

        List<UserEmployeeModel> GetEmployeeDetailsByDept(int deptId);

        UserEmployeeModel GetSingleEmployeeDetail(int employeeNumber);

        UserEmployeeModel GetSingleEmployeeDetailById(int id);
    }
}
