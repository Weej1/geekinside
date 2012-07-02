using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using DALFactory;
using IDAL;

namespace BLL
{
    public class BLLFolder
    {
        IDAL.IDALFolder folderDAL = DALFactory.DataAccess.CreateFolderDAL();

        public IList<FolderModel> GetAllFolders()
        {
            IDALFolder folderDAL= DALFactory.DataAccess.CreateFolderDAL();
            return folderDAL.GetFolderStructure();
        }

        public FolderModel GetFolderById(int folder_id)
        {
            return folderDAL.GetFolderById(folder_id);
        }

        public void UpdateFolder(FolderModel folder)
        {
            folderDAL.UpdateFolder(folder);
        }
    }
}

