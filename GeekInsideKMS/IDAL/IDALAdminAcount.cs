﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALAdminAcount
    {
        UserAdminModel getUserByUsername(string username);
    }
}
