using System;
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

        public static IDALDepartment CreateDepartmentDAL()
        {
            string className = path + ".DALDepartment";
            return (IDALDepartment)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALFolder CreateFolderDAL()
        {
            string className = path + ".DALFolder";
            return (IDALFolder)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALSearch CreateSearchDAL()
        {
            string className = path + ".DALSearch";
            return (IDALSearch)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALTag CreateTagDAL()
        {
            string className = path + ".DALTag";
            return (IDALTag)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALDocument CreateDocumentDAL()
        {
            string className = path + ".DALDocument";
            return (IDALDocument)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALFileType CreateFileTypeDAL()
        {
            string className = path + ".DALFileType";
            return (IDALFileType)Assembly.Load(path).CreateInstance(className);
        }

        public static IDALEmployeeDetail CreateEmployeeDetailDAL()
        {
            string className = path + ".DALEmployeeDetail";
            return (IDALEmployeeDetail)Assembly.Load(path).CreateInstance(className);
        }
    }
}
