using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLDocument
    {
        IDALDocument documentDAL = DALFactory.DataAccess.CreateDocumentDAL();

        public void DeleteTempFile(string id, string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf('.'));
            string filePath = Path.Combine("D://geekinsidekms/temp/", id + extension);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public bool AddDocument(DocumentModel document)
        {
            return true;
        }

        public List<DocumentModel> getMyCheckedDocList(int publisherNumber)
        {
            return documentDAL.getAllCheckedByPublisherNumber(publisherNumber);
        }

        public List<DocumentModel> getMyUnheckedDocList(int publisherNumber)
        {
            return documentDAL.getAllUncheckedByPublisherNumber(publisherNumber);
        }
    }
}
