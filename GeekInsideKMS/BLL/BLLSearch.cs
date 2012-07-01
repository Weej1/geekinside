using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLSearch:IDALSearch
    {
        public List<DocumentModel> getResultBySearchingTitle(string sw)
        {
            return null;
        }

        public List<DocumentModel> getResultBySearchingTag(string sw)
        {
            return null;
        }

        public List<DocumentModel> getResultBySearchingDescription(string sw)
        {
            return null;
        }

        public List<DocumentModel> getResultWithFilter(SearchFilterModel searchFilterModel)
        {
            return null;
        }
    }
}
