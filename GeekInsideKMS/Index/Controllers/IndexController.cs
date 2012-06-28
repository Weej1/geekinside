using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Index.Controllers
{
    public class IndexController : Controller
    {
        // GET: /Index/
        // 显示主页
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Index/Login
        // 登录界面
        public ActionResult Login()
        {
            return View();
        }

    }
}
