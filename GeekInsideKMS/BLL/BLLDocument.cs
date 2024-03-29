﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Transactions;
using Model.Models;
using IDAL;
using Utils;

namespace BLL
{
    public class BLLDocument
    {
        IDALDocument documentDAL = DALFactory.DataAccess.CreateDocumentDAL();
        BLLAuth bllAuth = new BLLAuth();
        public void DeleteTempFile(string id, string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf('.'));
            string filePath = Path.Combine(Helper.REPO_ROOT + "\\temp\\", id + extension);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        //将文档信息插入数据库
        public bool AddDocument(DocumentModel document, string[] tags, int employeeNumber)
        {
            IDALFileType fileTypeDAL = DALFactory.DataAccess.CreateFileTypeDAL();
            IDALFolder folderDAL = DALFactory.DataAccess.CreateFolderDAL();
            IDALUserAccount userDAL = DALFactory.DataAccess.CreateUserDAL();
            IDALEmployeeDetail employeeDAL = DALFactory.DataAccess.CreateEmployeeDetailDAL();
            IDALTag tagDAL = DALFactory.DataAccess.CreateTagDAL();
            int documentId;
            int tagId;

            string fileExtention = document.FileDisplayName.Substring(document.FileDisplayName.LastIndexOf(".") + 1);

            document.FileTypeId = fileTypeDAL.GetFileTypeId(fileExtention);
            document.PubTime = System.DateTime.Now;
            document.Size = Utils.FileSizeTransformer.TransformSize(Convert.ToInt32(document.Size));
            document.PublisherNumber = userDAL.getUserByEmployeeNumber(employeeNumber).EmployeeNumber;
            document.PublisherName = employeeDAL.GetUserEmployeeDetail(employeeNumber).Name;

            //建立分布式事务
            using (TransactionScope scope = new TransactionScope())
            {
                if (( documentId = documentDAL.CreateDocument(document)) != 0 )
                {
                    if (tags != null)
                    {
                        foreach (string tag in tags)
                        {
                            if ((tagId = tagDAL.GetTagIdByTagName(tag)) == 0)
                            {
                                tagId = tagDAL.AddTag(tag);
                            }
                            tagDAL.AddTagOfDoc(tagId, documentId);
                        }
                    }
                    string newFilePath = MoveFile(document.FileDiskName, folderDAL.GetFolderById(document.FolderId).PhysicalPath);
                    if (fileExtention == "pdf")
                    {
                        Helper.ConvertPdfToSwf(newFilePath);
                    }
                    if (fileExtention == "doc" ||
                        fileExtention == "docx"||
                        fileExtention == "xls" ||
                        fileExtention == "xlsx"||
                        fileExtention == "ppt" ||
                        fileExtention == "pptx")
                    {
                        //转换文件
                        Helper.ConvertDocumentToSwf(newFilePath);
                    }
                    scope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }           
            }
        }

        public List<DocumentModel> getMyCheckedDocList(int publisherNumber)
        {
            //带权限过滤的
            return bllAuth.documentFilter(publisherNumber, documentDAL.getAllCheckedByPublisherNumber(publisherNumber));
        }

        public List<DocumentModel> getMyUnheckedDocList(int publisherNumber)
        {
            return documentDAL.getAllUncheckedByPublisherNumber(publisherNumber);
        }

        private string MoveFile(string fileName, string physicalPath)
        {
            string filePath = Helper.REPO_ROOT + "\\temp\\" + fileName;
            string newFilePath = Helper.REPO_ROOT + physicalPath + "\\" + fileName;
            if (File.Exists(filePath))
            {
                File.Copy(filePath, newFilePath, true);
                File.Delete(filePath);
            }
            return newFilePath;
        }

        public DocumentModel getDocumentById(int docid)
        {
            return documentDAL.getDocumentById(docid);
        }

        public DocumentModel getDocumentByFileDiskName(string docname)
        {
            return documentDAL.getDocumentByFileDiskName(docname);
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

        //重载getAllToBeCheckedDoc 审核员不能看到自己发布的
        public List<DocumentModel> getAllToBeCheckedDoc(int employeeNumber)
        {
            return documentDAL.getAllToBeCheckedDoc(employeeNumber);
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
            //带权限过滤的
            return bllAuth.documentFilter(empno, documentDAL.getAllCheckedByPublisherNumber(empno));
        }

        public List<DocumentModel> getAllDocOrderByPubtime()
        {
            //不带权限过滤 这个函数是给管理员用的
            return documentDAL.getAllDocOrderByPubtime();
        }

        public Boolean updateDocument(DocumentModel docModel)
        {
            return documentDAL.updateDocument(docModel);
        }

        public List<DocumentModel> getDocByTagId(int empno,int tagid)
        {
            //带权限过滤的
            return bllAuth.documentFilter(empno, documentDAL.getDocByTagId(tagid));
        }

        public Boolean ViewNumberIncrement(int docId) 
        {
            return documentDAL.ViewNumberIncrement(docId);
        }

        public Boolean DownloadNumberIncrement(int docId) 
        {
            return documentDAL.DownloadNumberIncrement(docId);
        }

        //根据folderid和empno得到已按权限过滤的docModelList
        public List<DocumentModel> getDocByFolderId(int empno, int folder)
        {
            //带权限过滤的
            return bllAuth.documentFilter(empno, documentDAL.getDocByFolderId(folder));
        }

        //根据folderid、empno和页数（第几页）得到已按权限过滤的docModelList
        public List<DocumentModel> getDocByFolderId(int empno, int folder, int pageNumber)
        {
            //带权限过滤的
            int pageSize = 20;//默认每页20个
            return bllAuth.documentFilter(empno, documentDAL.getDocByFolderId(folder,pageNumber,pageSize));
        }
    }
}
