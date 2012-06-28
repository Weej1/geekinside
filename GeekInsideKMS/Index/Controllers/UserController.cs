using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Index.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /User/Workshop

        public ActionResult Workshop()
        {
            return View();
        }

        // GET: /User/Workshop

        public ActionResult Profile()
        {
            return View();
        }

    }
}
