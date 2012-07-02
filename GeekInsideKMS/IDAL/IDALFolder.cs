using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALFolder
    {
        /**
         * 创建目录（只修改DB，不包括文件系统的目录创建）
         * 一级目录的ParentFolderId需要为0
         * 返回新建的目录ID
         */ 
        int CreateFolder(FolderModel folder);

        /**
         * 更新单个目录的信息，允许改动的字段包括：
         * FolderName，Description，ParentFolderId和PhysicalPath
         */ 
        void UpdateFolder(FolderModel folder);

        /**
         * 删除单个目录，不级联删除子目录
         */ 
        void DeleteFolder(FolderModel folder);

        /**
         * 删除目录，并级联删除它的所有子目录
         * 返回删除的目录数
         */ 
        int DeleteFolderCascade(FolderModel folder);

        /**
         * 根据ID得到目录信息
         * 该方法获取目录本身，不会获取子目录信息
         */ 
        FolderModel GetFolderById(int folder_id);

        /**
         * 得到父目录的子目录
         * 注：不包括子目录的子目录，若想返回所有的子目录，请使用GetAllSubFolders
         * parentFolder 父目录
         */
        IList<FolderModel> GetSubFolders(FolderModel parentFolder);

        /**
         * 得到父目录的所有子目录
         * 注：返回的List中只包括第1级子目录，2级以后的目录通过父目录的SubFolders访问
         * parentFolder 父目录
         */
        IList<FolderModel> GetAllSubFolders(FolderModel parentFolder);

        /**
         * 返回所有1级目录，不包括它们的子目录
         */ 
        IList<FolderModel> GetTopFolders();

        /**
         * 得到整个目录结构
         * 返回所有1级目录及它们的所有子目录（通过SubFolders访问）
         */
        IList<FolderModel> GetFolderStructure();

    }
}
