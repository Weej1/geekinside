using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace DAL
{
    public class DALSiteNews : IDALSiteNews
    {
        public Boolean addNews(SiteNewsModel siteNewsModel)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.SiteNews siteNews = new DAL.SiteNews();
            siteNews.Title = siteNewsModel.Title;
            siteNews.NewsContent = siteNewsModel.NewsContent;
            siteNews.PubTime = siteNewsModel.PubTime;
            siteNews.IsOnTop = siteNewsModel.IsOnTop;
            context.AddToSiteNews(siteNews);
            context.SaveChanges();
            return true;
        }
        public Boolean updateNews(SiteNewsModel siteNewsModel)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.SiteNews dbNews = (from news in context.SiteNews
                                   where news.Id.Equals(siteNewsModel.Id)
                                   select news).FirstOrDefault();
            dbNews.Title = siteNewsModel.Title;
            dbNews.NewsContent = siteNewsModel.NewsContent;
            dbNews.PubTime = siteNewsModel.PubTime;
            dbNews.IsOnTop = siteNewsModel.IsOnTop;
            context.SaveChanges();
            return true;
        }
        public Boolean deleteNewsById(int id)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.SiteNews dbNews = (from news in context.SiteNews
                                   where news.Id.Equals(id)
                                   select news).FirstOrDefault();
            context.DeleteObject(dbNews);
            context.SaveChanges();
            return true;
        }
        public Boolean setItOnTopById(int id)
        {
            return true;
        }
        public List<SiteNewsModel> getAll()
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var siteNewsList = from n in gikms.SiteNews
                                   select n;
                List<DAL.SiteNews> newsTempList = siteNewsList.ToList();
                List<SiteNewsModel> newsList = new List<SiteNewsModel>();
                foreach (DAL.SiteNews nm in newsTempList)
                {
                    newsList.Add(new SiteNewsModel{
                        Id = nm.Id,
                        Title = nm.Title,
                        NewsContent = nm.NewsContent,
                        PubTime = nm.PubTime,
                        IsOnTop = nm.IsOnTop
                    });
                }
                return newsList;
            }
        }
        public SiteNewsModel getNewsById(int id)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                SiteNews dbNews = (from n in gikms.SiteNews
                                     where n.Id.Equals(id)
                                     select n).FirstOrDefault();
                if (dbNews == null) 
                    return null;
                return new SiteNewsModel
                {
                    Id = dbNews.Id,
                    Title = dbNews.Title,
                    NewsContent = dbNews.NewsContent,
                    IsOnTop = dbNews.IsOnTop,
                    PubTime = dbNews.PubTime
                };
            }
        }
    }
}
