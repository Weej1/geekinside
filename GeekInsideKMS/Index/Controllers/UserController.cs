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
        public ActionResult Workshop()
        {
            string empno = Session["username"].ToString();
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = Convert.ToInt32(empno);
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
        public ActionResult UnCheckedDocList()
        {
            string empno = Session["username"].ToString();
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = Convert.ToInt32(empno);
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
        public ActionResult MyFavorite()
        {
            string empno = Session["username"].ToString();
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = Convert.ToInt32(empno);
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
        [Authorize]
        public ActionResult DeleteFavorite(int docid)
        {
            string employeeNumber = Session["username"].ToString();
            BLLDocument bllDocument = new BLLDocument();
            DocumentModel docModel = bllDocument.getDocumentById(docid);
            if (employeeNumber == "" || docModel == null || docModel.PublisherNumber != Convert.ToInt32(employeeNumber))
            {
                TempData["errorMsg"] = "您无权进行删除操作，请重新登录。";
                return RedirectToAction("MyFavorite", "User");
            }
            BLLFavorite bllFavorite = new BLLFavorite();
            if (bllFavorite.deleteMyFavorite(Convert.ToInt32(employeeNumber),docid))
            {
                TempData["successMsg"] = "删除成功。";
            }
            else
            {
                TempData["errorMsg"] = "删除失败。";
            }
            return RedirectToAction("MyFavorite", "User");
        }

        //添加我的收藏
        [Authorize]
        public ActionResult addFavorite(int docid,string returnURL)
        {
            string employeeNumber = Session["username"].ToString();
            BLLDocument bllDocument = new BLLDocument();
            DocumentModel docModel = bllDocument.getDocumentById(docid);
            if (employeeNumber == "" || docModel == null || docModel.PublisherNumber != Convert.ToInt32(employeeNumber))
            {
                TempData["errorMsg"] = "您无权进行添加收藏操作，请重新登录。";
                return RedirectToAction(returnURL, "User");
            }
            BLLFavorite bllFavorite = new BLLFavorite();
            if (bllFavorite.addToMyFavorite(Convert.ToInt32(employeeNumber), docid))
            {
                TempData["successMsg"] = "添加成功。";
            }
            else
            {
                TempData["errorMsg"] = "添加失败。";
            }
            return RedirectToAction(returnURL, "User");
        }
    }
}
