using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using BLL;

namespace Index.Controllers
{
    public class NewsController : Controller
    {
        BLLSiteNews bllSiteNews = new BLLSiteNews();

        //公告列表
        public ActionResult Index(int pageNumber=1)
        {
            List<SiteNewsModel> newsList;
            newsList = bllSiteNews.getAll(pageNumber);
            ViewData["newsList"] = newsList;
            PageModel pageModel = new PageModel();
            pageModel.TotalCount = bllSiteNews.getTotalCount();
            pageModel.pageNumber = pageNumber;
            pageModel.pageSize = 2;
            ViewData["pageModel"] = pageModel;
            return View();
        }

        //公告详情
        public ActionResult Detail(int newsid)
        {
            SiteNewsModel snModel = bllSiteNews.getNewsById(newsid);
            ViewData["newsModel"] = snModel;
            return View();
        }
    }
}
