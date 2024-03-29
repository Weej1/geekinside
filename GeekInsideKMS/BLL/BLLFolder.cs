using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using DALFactory;
using IDAL;
using Utils;

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

        public IList<FolderModel> getAllFoldersByDepartmentId(int deptId)
        {
            DepartmentModel deptModel = new BLLDepartment().GetDepartment(deptId);
            FolderModel parentFolderModel = folderDAL.GetFolderById(deptModel.FolderId);
            return folderDAL.GetAllSubFolders(parentFolderModel);
        }

        public Boolean addFolder(FolderModel folderModel)
        {
            FolderModel parentFolder = GetFolderById(folderModel.ParentFolderId);
            folderModel.PhysicalPath = Helper.CreateNewFolderPath(parentFolder.PhysicalPath+"\\");
            Helper.CreateDirectory(folderModel.PhysicalPath);
            if (folderDAL.CreateFolder(folderModel) > 0)
            {
                return true;
            }
            return false;
        }

        public Boolean deleteFolderById(int folderId)
        {
            folderDAL.DeleteFolder(folderDAL.GetFolderById(folderId));
            return true;
        }

        public Boolean deleteFolderCascadeById(int folderId)
        {
            if (folderDAL.DeleteFolderCascade(folderDAL.GetFolderById(folderId)) > 0)
            {
                return true;
            }
            return false ;
        }

        IList<FolderModel> getSubfoldersByParentFolderId(int parentFolderId)
        {
            FolderModel parentFolder = GetFolderById(parentFolderId);
            return folderDAL.GetAllSubFolders(parentFolder);
        }
    }
}

