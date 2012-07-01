using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string FileDisplayName { get; set; }
        public string FileDiskName { get; set; }
        public string Description { get; set; }
        public int[] FileTagIdArray { get; set; }
        public int FolderId { get; set; }
        public int FileTypeId { get; set; }
        public string FileTypeName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public DateTime PubTime { get; set; }
        public int CheckerNumber { get; set; }
        public string CheckerName { get; set; }
        public string Size { get; set; }
        public int ViewNumber { get; set; }
        public int DownloadNumber { get; set; }
        public Boolean IsChecked { get; set; }
    }
}
