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

        public List<DocumentModel> getAllFavoriteDocListByPublisherNumber(int publisherNumber)
        {
            List<DocumentModel> favDocumentModelList = new List<DocumentModel>();
            List<FavoriteModel> favModelList = new List<FavoriteModel>();
            favModelList = new DALFavorite().getFavoriteDocListByEmployeeNumber(publisherNumber);
            foreach (FavoriteModel favModel in favModelList)
            {
                favDocumentModelList.Add(getDocumentById(favModel.DocumentId));
            }
            return favDocumentModelList;
        }

        public DocumentModel getDocumentById(int id)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                Document dbDocs = (from d in gikms.Documents
                                   where d.Id.Equals(id)
                                   select d).FirstOrDefault();
                if (dbDocs == null)
                {
                    return null;
                }
                List<TagModel> tagIdArray = new DALTag().getTagModelListByDocId(dbDocs.Id);
                return new DocumentModel
                {
                    Id = dbDocs.Id,
                    FileDisplayName = dbDocs.FileDisplayName,
                    FileDiskName = dbDocs.FileDiskName,
                    Description = dbDocs.Description,
                    FileTagIdArray = tagIdArray,
                    FolderId = dbDocs.FolderId,
                    FileTypeId = dbDocs.FileTypeId,
                    FileTypeName = dbDocs.FileTypeReference.Value.TypeName,
                    PublisherNumber = dbDocs.PublisherNumber,
                    PublisherName = dbDocs.PublisherName,
                    PubTime = dbDocs.PubTime,
                    CheckerNumber = dbDocs.CheckerNumber,
                    CheckerName = dbDocs.CheckerName,
                    Size = dbDocs.Size,
                    ViewNumber = dbDocs.ViewNumber,
                    DownloadNumber = dbDocs.DownloadNumber,
                    IsChecked = dbDocs.IsChecked
                 };
            }
        }

        public bool CreateDocument(DocumentModel document)
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
                try
                {
                    context.AddToDocuments(dbDocument);
                    context.SaveChanges();
                }
                catch(Exception e){
                    System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                    return false;
                }
                return true;
            }
        }
    }
}
