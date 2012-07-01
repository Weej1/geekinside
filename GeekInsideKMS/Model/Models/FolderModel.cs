using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class FolderModel
    {
        public int Id { get; set; }

        public string FolderName { get; set; }

        public string Description { get; set; }

        public int ParentFolderId { get; set; }

        public IList<FolderModel> SubFolders { get; set; }

        public string PhysicalPath { get; set; }
    }
}
