using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLSearch
    {
        IDALDocument documentDAL = DALFactory.DataAccess.CreateDocumentDAL();

        //基本搜索（标题和描述）
        public List<DocumentModel> getResultBasicSearch(string sw)
        {
            return documentDAL.getResultBySearchTitleAndDescription(sw);
        }

        //只搜索标题
        public List<DocumentModel> getResultBySearchingTitle(string sw)
        {
            return documentDAL.getDocModelListBySearchTitle(sw);
        }

        //只搜索tag
        public List<DocumentModel> getResultBySearchingTag(string sw)
        {
            return null;
        }

        //只搜索描述
        public List<DocumentModel> getResultBySearchingDescription(string sw)
        {
            return documentDAL.getDocModelListBySearchDescription(sw);
        }

        //高级搜索
        public List<DocumentModel> getResultWithFilter(SearchFilterModel searchFilterModel)
        {
            return documentDAL.getResultWithFilter(searchFilterModel);
        }
    }
}
