using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALDocument
    {
        bool CreateDocument(DocumentModel document);
        List<DocumentModel> getAllCheckedByPublisherNumber(int publisherNumber);
        List<DocumentModel> getAllUncheckedByPublisherNumber(int publisherNumber);
        List<DocumentModel> getAllFavoriteDocListByPublisherNumber(int publisherNumber);
        DocumentModel getDocumentById(int id);
        Boolean deleteDocumentById(int docid);
        List<DocumentModel> getToBeCheckedDocByCheckerNumber(int employeeNumber);
        List<DocumentModel> getHaveCheckedDocByCheckerNumber(int employeeNumber);
        Boolean setDocUncheckedById(int docid);
        Boolean setDocCheckedById(int docid);
    }
}
