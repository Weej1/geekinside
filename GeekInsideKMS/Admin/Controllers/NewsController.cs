using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model.Models;
using BLL;

namespace Admin.Controllers
{
    public class NewsController : Controller
    {
        BLLSiteNews bllSiteNews = new BLLSiteNews();

        [Authorize]
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

        [Authorize]
        public ActionResult Edit(int newsid)
        {
            SiteNewsModel snModel = bllSiteNews.getNewsById(newsid);
            ViewData["id"] = snModel.Id;
            ViewData["title"] = snModel.Title;
            ViewData["newscontent"] = snModel.NewsContent;
            ViewData["pubtime"] = snModel.PubTime;
            ViewData["isontop"] = snModel.IsOnTop.ToString();
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult doEdit()
        {
            SiteNewsModel siteNewsModel = new SiteNewsModel();
            siteNewsModel.Id = Convert.ToInt32(Request.Form["Id"]);
            siteNewsModel.Title = Request.Form["title"];
            siteNewsModel.NewsContent = Request.Form["newscontent"];
            siteNewsModel.PubTime = Convert.ToDateTime(Request.Form["pubtime"]);

            if (Request.Form["isontop"] != null && Request.Form["isontop"].Contains("on"))
            {
                siteNewsModel.IsOnTop = true;
            } 
            else
            {
                siteNewsModel.IsOnTop = false;
            }
            
            if (bllSiteNews.updateNews(siteNewsModel))
            {
                TempData["successMsg"] = "保存成功！";
                return RedirectToAction("Index", "News");
            }
            else
            {
                TempData["errorMsg"] = "保存失败！";
                return RedirectToAction("Index", "News");
            }
            
        }

        [Authorize]
        public ActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult doAddNews()
        {
            SiteNewsModel siteNewsModel = new SiteNewsModel();
            siteNewsModel.Title = Request.Form["title"];
            siteNewsModel.NewsContent = Request.Form["newscontent"];
            siteNewsModel.PubTime = DateTime.Now;
            if (Request.Form["isontop"] != null && Request.Form["isontop"].Contains("on"))
            {
                siteNewsModel.IsOnTop = true;
            }
            else
            {
                siteNewsModel.IsOnTop = false;
            }

            if (bllSiteNews.add(siteNewsModel))
            {
                TempData["successMsg"] = "保存成功！";
                return RedirectToAction("Index", "News");
            }
            else
            {
                TempData["errorMsg"] = "保存失败！";
                return RedirectToAction("Index", "News");
            }
        }

        [Authorize]
        public ActionResult Delete(int newsid)
        {
            if (bllSiteNews.delete(newsid))
            {
                TempData["successMsg"] = "删除成功！";
                return RedirectToAction("Index", "News");
            }
            else
            {
                TempData["errorMsg"] = "删除失败！";
                return RedirectToAction("Index", "News");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult deleteMultiNews(string[] selected_news)
        {

            Boolean result = true;
            foreach (string checkbox in selected_news)
            {
                if (!bllSiteNews.delete(Convert.ToInt32(checkbox)))
                {
                    result = false;
                }
            }
            if (result)
            {
                TempData["successMsg"] = "删除成功！";
                return RedirectToAction("Index", "News");
            }
            else
            {
                TempData["errorMsg"] = "删除失败！";
                return RedirectToAction("Index", "News");
            }
        }
    }
}
