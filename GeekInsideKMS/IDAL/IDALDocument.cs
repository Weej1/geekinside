using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALDocument
    {
        int CreateDocument(DocumentModel document);
        List<DocumentModel> getAllCheckedByPublisherNumber(int publisherNumber);
        List<DocumentModel> getAllUncheckedByPublisherNumber(int publisherNumber);
        List<DocumentModel> getAllFavoriteDocListByPublisherNumber(int publisherNumber);
        DocumentModel getDocumentById(int id);
        DocumentModel getDocumentByFileDiskName(string fileDiskName);
        Boolean deleteDocumentById(int docid);
        List<DocumentModel> getAllToBeCheckedDoc();
        List<DocumentModel> getAllToBeCheckedDoc(int employeeNumber);
        List<DocumentModel> getToBeCheckedDocByCheckerNumber(int employeeNumber);
        List<DocumentModel> getHaveCheckedDocByCheckerNumber(int employeeNumber);
        Boolean setDocUncheckedById(int docid);
        Boolean setDocCheckedById(int docid, int checkerEmpNumber);
        List<DocumentModel> getTopTenDocumentBy(string byWhat);
        List<DocumentModel> getDocModelListBySearchTitle(string sw);
        List<DocumentModel> getDocModelListBySearchDescription(string sw);
        List<DocumentModel> getResultBySearchTitleAndDescription(string sw);
        List<DocumentModel> getResultWithFilter(SearchFilterModel searchFilterModel);
        List<DocumentModel> getAllDocOrderByPubtime();
        List<DocumentModel> getDocByTagId(int tagid);
        List<DocumentModel> getDocByFolderId(int folderid);
        List<DocumentModel> getDocByFolderId(int folderid, int pageNumber, int pageSize);
        Boolean updateDocument(DocumentModel docModel);
        Boolean ViewNumberIncrement(int docId);
        Boolean DownloadNumberIncrement(int docId);
    }
}
