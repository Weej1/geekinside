using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;

namespace DAL
{
    public class DALFolder : IDALFolder
    {
        public int CreateFolder(Model.Models.FolderModel folder)
        {
            using (geekinsidekmsEntities context = new geekinsidekmsEntities())
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

        public void UpdateFolder(Model.Models.FolderModel folder)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(Model.Models.FolderModel folder)
        {
            throw new NotImplementedException();
        }

        public Model.Models.FolderModel GetFolderById(int folder_id)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                Folder dbFolder = (from f in gikms.Folders
                                        where f.Id.Equals(folder_id)
                                        select f).FirstOrDefault();
                return ConvertFromDB(dbFolder);
            }
        }

        public IList<Model.Models.FolderModel> GetSubFolders(Model.Models.FolderModel folder)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Models.FolderModel> GetTopFolders()
        {
            throw new NotImplementedException();
        }

        private FolderModel ConvertFromDB(Folder dbFolder)
        {
            if (dbFolder == null) return null;
            return new FolderModel
            {
                Id = dbFolder.Id,
                ParentFolderId = dbFolder.ParentId,
                PhysicalPath = dbFolder.PhysicalPath,
                FolderName = dbFolder.FolderName,
                Description = dbFolder.Description
            };
        }

    }
}
