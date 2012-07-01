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
    }
}
