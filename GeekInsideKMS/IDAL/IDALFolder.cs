using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALFolder
    {
        int CreateFolder(FolderModel folder);

        void UpdateFolder(FolderModel folder);

        void DeleteFolder(FolderModel folder);

        FolderModel GetFolderById(int folder_id);

        IList<FolderModel> GetSubFolders(FolderModel folder);

        IList<FolderModel> GetTopFolders();
    }
}
