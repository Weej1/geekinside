using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using BLL;
using System.Web.Security;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        //登录
        [HttpPost]
        public ActionResult Login(UserAdminModel userAdminModel)
        {
            BLLAdminAcount bllAdminAccount = new BLLAdminAcount();
            Boolean result = bllAdminAccount.CheckAdminLogin(userAdminModel);
            if (result == true)
            {
                FormsAuthentication.SetAuthCookie(userAdminModel.Username, true);
                return RedirectToAction("Index", "Index");
            } 
            else
            {
                ViewData["errorMsg"] = "用户名和密码错误";
                return View();
            }
        }

        //注销
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Index");
        }
    }
}
