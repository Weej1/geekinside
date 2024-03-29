﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using System.Web.Security;
using BLL;

namespace Index.Controllers
{
    public class SearchController : Controller
    {


        //高级搜索输入界面
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //简单搜索
        [HttpPost]
        [Authorize]
        public ActionResult doBasicSearch()
        {
            string sw = Request.Form["sw"];
            List<DocumentModel> docList = new BLLSearch().getResultBasicSearch(sw);
            if (docList.Count == 0)
            {
                ViewData["docList"] = "nodata";
            }
            else
            {
                ViewData["docList"] = docList;
            }
            return View("BasicSearchResult");
        }

        //高级搜索
        [HttpPost]
        [Authorize]
        public ActionResult doAdvanceSearch(string sw, string sw_notinclude, string[] sw_doctype)
        {
            if (sw == null)
            {
                sw = "";
            }
            if (sw_notinclude == null)
            {
                sw_notinclude = "";
            }
            if (sw_doctype == null)
            {
                sw_doctype = new string[0];
            }
            List<DocumentModel> docList = new BLLSearch().getResultWithFilter(new SearchFilterModel
            {
                sw = sw,
                sw_notincluded = sw_notinclude,
                sw_doctype = sw_doctype
            });
            if (docList.Count == 0)
            {
                ViewData["docList"] = "nodata";
            }
            else
            {
                ViewData["docList"] = docList;
            }
            return View("BasicSearchResult");
        }
    }
}
