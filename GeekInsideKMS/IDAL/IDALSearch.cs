using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALSearch
    {
        List<DocumentModel> getResultBySearchingTitle(string sw);
        List<DocumentModel> getResultBySearchingTag(string sw);
        List<DocumentModel> getResultBySearchingDescription(string sw);
        List<DocumentModel> getResultWithFilter(SearchFilterModel searchFilterModel);
    }
}
