using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Model.Models;
using IDAL;
using Utils;

namespace BLL
{
    public class BLLDocument
    {
        IDALDocument documentDAL = DALFactory.DataAccess.CreateDocumentDAL();

        public void DeleteTempFile(string id, string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf('.'));
            string filePath = Path.Combine(Helper.REPO_ROOT + "\\temp\\", id + extension);
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
            document.PublisherNumber = userDAL.getUserByEmployeeNumber(employeeNumber).EmployeeNumber;
            document.PublisherName = employeeDAL.GetUserEmployeeDetail(employeeNumber).Name;

            if (documentDAL.CreateDocument(document))
            {
                MoveFile(document.FileDiskName, folderDAL.GetFolderById(document.FolderId).PhysicalPath);
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

        private void MoveFile(string fileName, string physicalPath)
        {
            string filePath = Helper.REPO_ROOT + "\\temp\\" + fileName;
            string newFilePath = Helper.REPO_ROOT + physicalPath + "\\" + fileName;
            if (File.Exists(filePath))
            {
                File.Copy(filePath, newFilePath, false);
                File.Delete(filePath);
            }
        }

        public DocumentModel getDocumentById(int docid)
        {
            return documentDAL.getDocumentById(docid);
        }

        public Boolean deleteDocumentById(int docid)
        {
            return documentDAL.deleteDocumentById(docid);
        }

        public List<DocumentModel> getToBeCheckedDocByCheckerNumber(int employeeNumber)
        {
            return documentDAL.getToBeCheckedDocByCheckerNumber(employeeNumber);
        }

        public List<DocumentModel> getAllToBeCheckedDoc()
        {
            return documentDAL.getAllToBeCheckedDoc();
        }

        public List<DocumentModel> getHaveCheckedDocByCheckerNumber(int employeeNumber)
        {
            return documentDAL.getHaveCheckedDocByCheckerNumber(employeeNumber);
        }

        public Boolean setDocUncheckedById(int docid)
        {
            return documentDAL.setDocUncheckedById(docid);
        }

        public Boolean setDocCheckedById(int docid,int checkerEmpNumber)
        {
            return documentDAL.setDocCheckedById(docid, checkerEmpNumber);
        }

        public List<DocumentModel> getTopTenDocumentByViewNumber()
        {
            return documentDAL.getTopTenDocumentBy("ViewNumber");
        }

        public List<DocumentModel> getTopTenDocumentByDownloadNumber()
        {
            return documentDAL.getTopTenDocumentBy("DownloadNumber");
        }

        public List<DocumentModel> GetDocByEmpployeeNumber(int empno)
        {
            return documentDAL.getAllCheckedByPublisherNumber(empno);
        }

        public List<DocumentModel> getAllDocOrderByPubtime()
        {
            return documentDAL.getAllDocOrderByPubtime();
        }

        public Boolean updateDocument(DocumentModel docModel)
        {
            return documentDAL.updateDocument(docModel);
        }

        public List<DocumentModel> getDocByTagId(int tagid)
        {
            return documentDAL.getDocByTagId(tagid);
        }
    }
}
