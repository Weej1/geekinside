using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using System.Web.Security;
using BLL;

namespace Index.Controllers
{
    public class UserController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //我发布的文档列表
        [Authorize]
        public ActionResult Workshop(int empno)
        {
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = empno;
            docList = new BLLDocument().getMyCheckedDocList(employeeNumber);
            if (docList.Count == 0)
            {
                ViewData["docList"] = "nodata";
            } 
            else
            {
                ViewData["docList"] = docList;
            }
            return View();
        }

        //我的资料修改
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

        //我的未审核文档
        [Authorize]
        public ActionResult UnCheckedDocList(int empno)
        {
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = empno;
            docList = new BLLDocument().getMyUnheckedDocList(employeeNumber);
            if (docList.Count == 0)
            {
                ViewData["docList"] = "nodata";
            }
            else
            {
                ViewData["docList"] = docList;
            }
            return View();
        }

        [Authorize]
        public ActionResult MyFavorite(int empno)
        {
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = empno;
            docList = new BLLFavorite().getFavoriteDocModelListByPublishNumber(employeeNumber);
            if (docList.Count == 0)
            {
                ViewData["docList"] = "nodata";
            }
            else
            {
                ViewData["docList"] = docList;
            }
            return View();
        }

        //删除我的收藏
        //要先判断当前用户有权删除没有
        [Authorize]
        public ActionResult DeleteFavorite(int docid)
        {
            return null;
        }
    }
}
