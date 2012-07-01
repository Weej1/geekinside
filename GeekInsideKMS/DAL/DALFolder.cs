using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;

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
                    ParentId = folder.UpFolderId,
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
            throw new NotImplementedException();
        }

        public IList<Model.Models.FolderModel> GetSubFolders(Model.Models.FolderModel folder)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Models.FolderModel> GetTopFolders()
        {
            throw new NotImplementedException();
        }
    }
}
