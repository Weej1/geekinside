using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using System.Web.Security;

namespace Index.Controllers
{
    public class UserController : Controller
    {


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Workshop()
        {

            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            //待写
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult doProfile(UserEmployeeModel userEmployeeModel)
        {
            //待写
            return View();
        }

        [Authorize]
        public ActionResult UnCheckedDocList()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyFavorite()
        {
            return View();
        }
    }
}
