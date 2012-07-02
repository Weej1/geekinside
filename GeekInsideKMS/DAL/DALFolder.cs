using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;
using System.Data;

namespace DAL
{
    public class DALFolder : IDALFolder
    {
        private Folder ConvertModelToDB(FolderModel model)
        {
            return new Folder
            {
                Id = model.Id,
                FolderName = model.FolderName,
                Description = model.Description,
                ParentId = model.ParentFolderId,
                PhysicalPath = model.PhysicalPath
            };
        }

        private FolderModel ConvertDBToModel(Folder db)
        {
            return new FolderModel
            {
                Id = db.Id,
                FolderName = db.FolderName,
                ParentFolderId = db.ParentId,
                Description = db.Description,
                PhysicalPath = db.PhysicalPath
            };
        }

        public int CreateFolder(FolderModel folder)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                Folder dbFolder = new Folder
                {
                    FolderName = folder.FolderName,
                    Description = folder.Description,
                    ParentId = folder.ParentFolderId,
                    PhysicalPath = folder.PhysicalPath
                };
                context.Folders.AddObject(dbFolder);
                context.SaveChanges();
                return dbFolder.Id;
            }
        }

        public void UpdateFolder(FolderModel folder)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                Folder dbFolder = ConvertModelToDB(folder);
                context.Folders.AddObject(dbFolder);
                context.ObjectStateManager.ChangeObjectState(dbFolder, EntityState.Modified);
                context.SaveChanges();
            }
        }

        public void DeleteFolder(FolderModel folder)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                Folder dbFolder = ConvertModelToDB(folder);
                context.Folders.Attach(dbFolder);
                context.Folders.DeleteObject(dbFolder);
                context.SaveChanges();
            }
        }

        public int DeleteFolderCascade(FolderModel folder)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                folder.SubFolders = GetAllSubFolders(folder);
                // DFS遍历目录结构
                Stack<FolderModel> folder_to_delete = new Stack<FolderModel>();
                folder_to_delete.Push(folder);
                while (folder_to_delete.Count != 0)
                {
                    FolderModel current = folder_to_delete.Pop();
                    foreach (FolderModel child in current.SubFolders)
                    {
                        folder_to_delete.Push(child);
                    }
                    Folder dbCurrent = ConvertModelToDB(current);
                    context.Folders.Attach(dbCurrent);
                    context.Folders.DeleteObject(dbCurrent);
                }
                return context.SaveChanges();
            }
        }

        public FolderModel GetFolderById(int folder_id)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                Folder dbFolder = (from f in context.Folders
                                   where f.Id == folder_id
                                   select f).FirstOrDefault();
                if (dbFolder == null) return null;
                return ConvertDBToModel(dbFolder);
            }
        }

        public IList<FolderModel> GetSubFolders(FolderModel parentFolder)
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                IList<FolderModel> result = new List<FolderModel>();
                var list = from f in context.Folders
                           where f.ParentId == parentFolder.Id
                           select f;
                foreach (Folder subfolder in list)
                {
                    result.Add(ConvertDBToModel(subfolder));
                }
                return result;
            }
        }

        public IList<FolderModel> GetAllSubFolders(FolderModel parentFolder)
        {
            IList<FolderModel> firstLevelSubFolders = GetSubFolders(parentFolder);
            // 遍历子目录
            Stack<FolderModel> foldersToVisit = new Stack<FolderModel>(firstLevelSubFolders);
            while (foldersToVisit.Count != 0)
            {
                FolderModel current = foldersToVisit.Pop();
                current.SubFolders = GetSubFolders(current);
                foreach (FolderModel sub in current.SubFolders)
                {
                    foldersToVisit.Push(sub);
                }
            }
            return firstLevelSubFolders;
        }

        public IList<FolderModel> GetTopFolders()
        {
            using (geekinsidekmsEntities context =
                new geekinsidekmsEntities())
            {
                IList<FolderModel> result = new List<FolderModel>();
                var tops = from f in context.Folders
                           where f.ParentId == 0
                           select f;
                foreach (Folder dbItem in tops)
                {
                    result.Add(ConvertDBToModel(dbItem));
                }
                return result;
            }
        }

        public IList<FolderModel> GetFolderStructure()
        {
            IList<FolderModel> topFolders = GetTopFolders();
            foreach (FolderModel topFolder in topFolders)
            {
                topFolder.SubFolders = GetAllSubFolders(topFolder);
            }
            return topFolders;
        }
    }
}
