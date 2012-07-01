using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using System.Web.Security;

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
            
            return View();
        }

        //高级搜索
        [HttpPost]
        [Authorize]
        public ActionResult doAdvancedSearch()
        {
            return View();
        }
    }
}
