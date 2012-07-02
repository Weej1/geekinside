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
                return gernerateFinalDocumentModelList(docTempList);
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
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        public List<DocumentModel> getAllFavoriteDocListByPublisherNumber(int publisherNumber)
        {
            List<DocumentModel> favDocumentModelList = new List<DocumentModel>();
            List<FavoriteModel> favModelList = new List<FavoriteModel>();
            favModelList = new DALFavorite().getFavoriteDocListByEmployeeNumber(publisherNumber);
            if (favModelList == null)
            {
                return favDocumentModelList;
            }
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

        public Boolean deleteDocumentById(int docid)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();
            //如果这篇文档被人收藏了，要先删除收藏，否则会报错
            List<DAL.Favorite> dbFavList = (from fav in context.Favorites
                                            where fav.DocumentId.Equals(docid)
                                            select fav).ToList();
            if (dbFavList.Count > 0)
            {
                DALFavorite dalFavorite = new DALFavorite();
                foreach (DAL.Favorite fav in dbFavList)
                {
                    dalFavorite.deleteFavById(fav.EmployeeNumber, fav.DocumentId);
                }
            } 
            else
            {
                DAL.Document dbDoc = (from doc in context.Documents
                                      where doc.Id.Equals(docid)
                                      select doc).FirstOrDefault();
                context.DeleteObject(dbDoc);

                context.SaveChanges();
            }
            
            return true;
        }

        public List<DocumentModel> getAllToBeCheckedDoc()
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocList = from d in gikms.Documents
                                where d.IsChecked.Equals(false)
                                select d;
                List<DAL.Document> docTempList = dbDocList.ToList();

                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        public List<DocumentModel> getToBeCheckedDocByCheckerNumber(int employeeNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocList = from d in gikms.Documents
                                where d.IsChecked.Equals(false) && d.CheckerNumber.Equals(employeeNumber)
                                select d;
                List<DAL.Document> docTempList = dbDocList.ToList();

                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        public List<DocumentModel> getHaveCheckedDocByCheckerNumber(int employeeNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocList = from d in gikms.Documents
                                where d.IsChecked.Equals(true) && d.CheckerNumber.Equals(employeeNumber)
                                select d;
                List<DAL.Document> docTempList = dbDocList.ToList();

                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        public Boolean setDocUncheckedById(int docid)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Document dbDoc = (from d in context.Documents
                                  where d.Id.Equals(docid)
                                  select d).FirstOrDefault();
            dbDoc.IsChecked = false;
            context.SaveChanges();
            return true;
        }

        public Boolean setDocCheckedById(int docid, int checkerEmpNumber)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Document dbDoc = (from d in context.Documents
                                  where d.Id.Equals(docid)
                                  select d).FirstOrDefault();
            dbDoc.IsChecked = true;
            dbDoc.CheckerNumber = checkerEmpNumber;
            context.SaveChanges();
            return true;
        }

        public List<DocumentModel> getTopTenDocumentBy(string byWhat)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                if (byWhat == "ViewNumber")
                {
                    docTempList = (from d in gikms.Documents
                                   where d.IsChecked.Equals(true)
                                   orderby d.ViewNumber descending
                                   select d).Take(10).ToList();
                }
                else
                {
                    docTempList = (from d in gikms.Documents
                                   where d.IsChecked.Equals(true)
                                   orderby d.DownloadNumber descending
                                   select d).Take(10).ToList();
                }

                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        //搜索标题
        public List<DocumentModel> getDocModelListBySearchTitle(string sw)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                docTempList = (from d in gikms.Documents
                               where d.FileDisplayName.Contains(sw)
                               select d).ToList();
                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        //搜索描述
        public List<DocumentModel> getDocModelListBySearchDescription(string sw)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                docTempList = (from d in gikms.Documents
                               where d.Description.Contains(sw)
                               select d).ToList();
                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        //基本搜索
        public List<DocumentModel> getResultBySearchTitleAndDescription(string sw)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                docTempList = (from d in gikms.Documents
                               where d.FileDisplayName.Contains(sw) || d.Description.Contains(sw)
                               select d).ToList();
                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        //得到所有文档
        public List<DocumentModel> getAllDocOrderByPubtime()
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocList = from d in gikms.Documents
                                orderby d.PubTime descending
                                select d;
                List<DAL.Document> docTempList = dbDocList.ToList();

                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        //生成最终的DocumentModelList 公用
        private List<DocumentModel> gernerateFinalDocumentModelList(List<DAL.Document> docTempList)
        {
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

        //更新单个文档
        public Boolean updateDocument(DocumentModel docModel)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Document dbDoc = (from doc in context.Documents
                                  where doc.Id.Equals(docModel.Id)
                                  select doc).FirstOrDefault();
            dbDoc.FileDisplayName = docModel.FileDisplayName;
            dbDoc.Description = docModel.Description;
            context.SaveChanges();
            return true;
        }
    }
}
