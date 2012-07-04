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
                    IsChecked = dbDocs.IsChecked,
                    AuthLevel = dbDocs.AuthLevel
                 };
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
                    FolderId = document.FolderId,
                    AuthLevel = document.AuthLevel
                };
                try
                {
                    context.AddToDocuments(dbDocument);
                    context.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                }
                catch(Exception e){
                    System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                }
                return dbDocument.Id;
            }
        }

        public Boolean deleteDocumentById(int docid)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();
            
            DAL.Document dbDoc = (from doc in context.Documents
                                  where doc.Id.Equals(docid)
                                  select doc).FirstOrDefault();
            context.DeleteObject(dbDoc);
            context.SaveChanges();
            
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

        //高级搜索
        public List<DocumentModel> getResultWithFilter(SearchFilterModel searchFilterModel)
        {
            string sw = searchFilterModel.sw;
            string sw_notincluede = searchFilterModel.sw_notincluded;
            string[] sw_doctype = searchFilterModel.sw_doctype;
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                if (!sw.Equals(""))
                {
                    docTempList = (from d in gikms.Documents
                               where (d.FileDisplayName.Contains(sw) || d.Description.Contains(sw))
                               select d).ToList();
                }
                if (!sw_notincluede.Equals(""))
                {
                    docTempList = (from d in docTempList
                                   where !d.FileDisplayName.Contains(sw_notincluede)
                                   select d).ToList();
                    docTempList = (from d in docTempList
                                   where !d.Description.Contains(sw_notincluede)
                                   select d).ToList();
                }
                if (!sw_doctype.Equals(null))
                {
                    foreach (string type in sw_doctype)
                    {
                        docTempList = (from d in docTempList
                                       where (d.FileTypeReference.Value.TypeName.Contains(type))
                                       select d).ToList();
                    }
                    
                }
                
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
                    IsChecked = doc.IsChecked,
                    AuthLevel = doc.AuthLevel
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
            dbDoc.IsChecked = docModel.IsChecked;
            context.SaveChanges();
            return true;
        }

        public List<DocumentModel> getDocByTagId(int tagid)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbDocIdList = (from did in gikms.DocumentTags
                                   where did.TagId.Equals(tagid)
                                   select did).ToList();
                List<DAL.Document> docTempList =  new List<DAL.Document>();
                foreach (var did in dbDocIdList)
                {
                    var dbDoc = (from d in gikms.Documents
                                 where d.Id.Equals(did.DocumentId)
                                 select d).FirstOrDefault();
                    docTempList.Add((DAL.Document)dbDoc);
                }

                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);

            }
        }
    
        //ViewNumber自增
        public Boolean ViewNumberIncrement(int docId) 
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Document dbDoc = (from doc in context.Documents
                                  where doc.Id.Equals(docId)
                                  select doc).FirstOrDefault();
            dbDoc.ViewNumber = dbDoc.ViewNumber + 1;
            context.SaveChanges();
            return true;
        }

        //DownloadNumber自增
        public Boolean DownloadNumberIncrement(int docId)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Document dbDoc = (from doc in context.Documents
                                  where doc.Id.Equals(docId)
                                  select doc).FirstOrDefault();
            dbDoc.DownloadNumber = dbDoc.DownloadNumber + 1;
            context.SaveChanges();
            return true;
        }

        //根据folderid得到doc
        public List<DocumentModel> getDocByFolderId(int folderid)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                docTempList = (from d in gikms.Documents
                               where d.FolderId.Equals(folderid)
                               select d).ToList();
                //生成最终List
                return gernerateFinalDocumentModelList(docTempList);
            }
        }

        //根据folderid和分页参数得到doc
        public List<DocumentModel> getDocByFolderId(int folderid, int pageNumber, int pageSize)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                List<DAL.Document> docTempList = new List<DAL.Document>();
                docTempList = (from d in gikms.Documents
                               where d.FolderId.Equals(folderid)
                               select d).ToList();
                //分页
                int totalCount = docTempList.Count();
                List<DAL.Document> docListPaged = new List<DAL.Document>();
                docListPaged = (from n in docTempList
                                select n).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                //生成最终List
                return gernerateFinalDocumentModelList(docListPaged);
            }
        }


        public DocumentModel getDocumentByFileDiskName(string fileDiskName)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                Document dbDocs = (from d in gikms.Documents
                                   where d.FileDiskName.Equals(fileDiskName)
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
                    IsChecked = dbDocs.IsChecked,
                    AuthLevel = dbDocs.AuthLevel
                };
            }
        }
    }
}
