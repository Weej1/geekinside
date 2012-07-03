using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using BLL;
using System.Web.Security;

namespace Index.Controllers
{
    public class IndexController : Controller
    {
        // GET: /Index/
        // 显示主页
        [Authorize]
        public ActionResult Index()
        {
            List<DocumentModel> viewdocList = new BLLDocument().getTopTenDocumentByViewNumber();
            ViewData["viewTop10Doc"] = viewdocList;
            List<DocumentModel> dldocList = new BLLDocument().getTopTenDocumentByDownloadNumber();
            ViewData["dlTop10Doc"] = dldocList;
            List<TagModel> tagModel = new BLLTag().getTop50TagModelList();
            ViewData["tagModel"] = tagModel;
            return View();
        }

        // GET: /Index/Login
        // 登录界面
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserEmployeeModel userEmployeeModel)
        {
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            Boolean result = bllUserAccount.CheckUserLogin(userEmployeeModel);
            if (result == true)
            {
                FormsAuthentication.SetAuthCookie(Convert.ToString(userEmployeeModel.EmployeeNumber), false);
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
            return RedirectToAction("Login", "Index");
        }
    }
}
