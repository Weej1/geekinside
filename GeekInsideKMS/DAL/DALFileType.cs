using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;

namespace DAL
{
    class DALFileType : IDALFileType
    {
        public int GetFileTypeId(string fileTypeName)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                int id = (from f in gikms.FileTypes
                          where f.TypeName.Equals(fileTypeName)
                              select f.Id).FirstOrDefault();
                return id;
            }
        }
    }
}
