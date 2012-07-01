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

        public bool AddDocument(DocumentModel document, int employeeNumber)
        {
            IDALFileType fileTypeDAL = DALFactory.DataAccess.CreateFileTypeDAL();
            IDALFolder folderDAL = DALFactory.DataAccess.CreateFolderDAL();
            IDALUserAccount userDAL = DALFactory.DataAccess.CreateUserDAL();
            IDALEmployeeDetail employeeDAL = DALFactory.DataAccess.CreateEmployeeDetailDAL();

            document.FileTypeId = fileTypeDAL.GetFileTypeId(document.FileDisplayName.Substring(document.FileDisplayName.LastIndexOf(".")+1));
            document.PubTime = System.DateTime.Now;
            document.Size = Utils.FileSizeTransformer.TransformSize(Convert.ToInt32(document.Size));
            document.PublisherNumber = userDAL.getUserByEmployeeNumber(employeeNumber).Id;
            document.PublisherName = employeeDAL.GetUserEmployeeDetail(employeeNumber).Name;

            if (documentDAL.CreateDocument(document) != null)
            {
                MoveFile(document.FileDiskName, folderDAL.GetFolderById(document.FolderId).FolderName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DocumentModel> getMyCheckedDocList(int publisherNumber)
        {
            return documentDAL.getAllCheckedByPublisherNumber(publisherNumber);
        }

        public List<DocumentModel> getMyUnheckedDocList(int publisherNumber)
        {
            return documentDAL.getAllUncheckedByPublisherNumber(publisherNumber);
        }

        private void MoveFile(string fileName, string folderName)
        {
            string filePath = "D;//geekinsidekms/temp/" + fileName;
            string newFilePath = "D;//geekinsidekms/" + folderName + "/" + fileName;
            if (File.Exists(filePath))
            {
                File.Copy(filePath, newFilePath, false);
                File.Delete(filePath);
            }
        }
    }
}
