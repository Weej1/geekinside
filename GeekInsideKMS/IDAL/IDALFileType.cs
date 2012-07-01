using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IDALFileType
    {
        int GetFileTypeId(string fileTypeName);
    }
}
