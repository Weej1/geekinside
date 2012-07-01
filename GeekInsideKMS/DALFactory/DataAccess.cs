﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using IDAL;

namespace DALFactory
{
    public class DataAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private DataAccess() {}

        public static IDALAdminAccount CreateAdminDAL()
        {
            string className = path + ".DALAdminAccount";
            return (IDALAdminAccount)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALUserAccount CreateUserDAL()
        {
            string className = path + ".DALUserAccount";
            return (IDALUserAccount)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALSiteConfig CreateSiteConfiguraionDAL()
        {
            string className = path + ".DALSiteConfig";
            return (IDALSiteConfig)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALSiteNews CreateSiteNewsDAL()
        {
            string className = path + ".DALSiteNews";
            return (IDALSiteNews)Assembly.Load(path).CreateInstance(className);
        }
    }
}
