using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace DAL
{
    public class DALDocument : IDALDocument
    {
        public List<DocumentModel> getAllCheckedByPublisherNumber(int publisherNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocList = from d in gikms.Documents
                                where d.PublisherNumber.Equals(publisherNumber) && d.IsChecked.Equals(true)
                                select d;

                if (dbDocList == null)
                {
                    return null;
                }

                List<DAL.Document> docTempList = dbDocList.ToList();

                //生成最终List
                List<DocumentModel> docList = new List<DocumentModel>();
                foreach (DAL.Document doc in docTempList)
                {
                    List<TagModel> tagIdArray = new DALTag().getTagModelListByDocId(doc.Id);
                    docList.Add(new DocumentModel
                    {
                        Id = doc.Id,
                        FileDisplayName = doc.FileDisplayName,
                        FileDiskName = doc.FileDiskName,
                        Description = doc.Description,
                        FileTagIdArray = tagIdArray,
                        FolderId = doc.FolderId,
                        FileTypeId = doc.FileTypeId,
                        FileTypeName = doc.FileTypeReference.Value.TypeName,
                        PublisherNumber = doc.PublisherNumber,
                        PublisherName = doc.PublisherName,
                        PubTime = doc.PubTime,
                        CheckerNumber  = doc.CheckerNumber,
                        CheckerName = doc.CheckerName,
                        Size = doc.Size,
                        ViewNumber = doc.ViewNumber,
                        DownloadNumber = doc.DownloadNumber,
                        IsChecked = doc.IsChecked
                    });
                }
                return docList;
            }
        }

        public List<DocumentModel> getAllUncheckedByPublisherNumber(int publisherNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocList = from d in gikms.Documents
                                where d.PublisherNumber.Equals(publisherNumber) && d.IsChecked.Equals(false)
                                select d;
                List<DAL.Document> docTempList = dbDocList.ToList();

                //生成最终List
                List<DocumentModel> docList = new List<DocumentModel>();
                foreach (DAL.Document doc in docTempList)
                {
                    List<TagModel> tagIdArray = new DALTag().getTagModelListByDocId(doc.Id);
                    docList.Add(new DocumentModel
                    {
                        Id = doc.Id,
                        FileDisplayName = doc.FileDisplayName,
                        FileDiskName = doc.FileDiskName,
                        Description = doc.Description,
                        FileTagIdArray = tagIdArray,
                        FolderId = doc.FolderId,
                        FileTypeId = doc.FileTypeId,
                        FileTypeName = doc.FileTypeReference.Value.TypeName,
                        PublisherNumber = doc.PublisherNumber,
                        PublisherName = doc.PublisherName,
                        PubTime = doc.PubTime,
                        CheckerNumber = doc.CheckerNumber,
                        CheckerName = doc.CheckerName,
                        Size = doc.Size,
                        ViewNumber = doc.ViewNumber,
                        DownloadNumber = doc.DownloadNumber,
                        IsChecked = doc.IsChecked
                    });
                }
                return docList;
            }
        }


        public int CreateDocument(DocumentModel document)
        {
            using (geekinsidekmsEntities context = new geekinsidekmsEntities())
            {
                Document dbDocument = new Document
                {
                    CheckerName = "",
                    Description = document.Description,
                    FileDiskName = document.FileDiskName,
                    FileDisplayName = document.FileDisplayName,
                    Size = document.Size,
                    PubTime = document.PubTime,
                    PublisherName = document.PublisherName,
                    PublisherNumber = document.PublisherNumber,
                    FileTypeId = document.FileTypeId,
                    FolderId = document.FolderId
    
                };
                context.AddToDocuments(dbDocument);
                context.SaveChanges();
                return dbDocument.Id;
            }
        }
    }
}
