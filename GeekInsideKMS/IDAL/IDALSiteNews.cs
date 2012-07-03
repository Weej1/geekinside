using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALSiteNews
    {
        Boolean addNews(SiteNewsModel siteNewsModel);
        Boolean updateNews(SiteNewsModel siteNewsModel);
        Boolean deleteNewsById(int id);
        Boolean setItOnTopById(int id);
        Boolean setItNotOnTopById(int id);
        List<SiteNewsModel> getAll();
        SiteNewsModel getNewsById(int id);
    }
}
