using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLSiteNews
    {
        IDALSiteNews siteNewsDAL = DALFactory.DataAccess.CreateSiteNewsDAL();

        //得到公告列表
        public List<SiteNewsModel> getAll()
        {
            List<SiteNewsModel> newsList = siteNewsDAL.getAll();
            return newsList;
        }

        //得到某篇公告内容
        public SiteNewsModel getNewsById(int id)
        {
            return siteNewsDAL.getNewsById(id);
        }

        //更新公告内容
        public Boolean updateNews(SiteNewsModel siteNewsModel)
        {
            return siteNewsDAL.updateNews(siteNewsModel);
        }

        //增加公告
        public Boolean add(SiteNewsModel siteNewsModel)
        {
            return siteNewsDAL.addNews(siteNewsModel);
        }

        //删除
        public Boolean delete(int id)
        {
            return siteNewsDAL.deleteNewsById(id);
        }

        //设置为置顶
        public Boolean setItOnTop(int id)
        {
            return siteNewsDAL.setItOnTopById(id);
        }

        //取消置顶
        public Boolean setItNotOnTop(int id)
        {
            return siteNewsDAL.setItNotOnTopById(id);
        }
    }
}
