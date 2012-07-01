using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Models;
using System.Web.Security;

namespace Index.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/
        //相当于我发布的文档
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Document/Detail
        [Authorize]
        public ActionResult Detail()
        {
            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {
            return View("DocumentUpload");
        }

        //编辑文档信息
        [Authorize]
        public ActionResult Edit(int docid,string returnURL)
        {
            //先要判断当前用户是否有权限编辑这篇文档（是不是发布者）

            return RedirectToAction(returnURL, "User");
        }

        //删除文档
        [Authorize]
        public ActionResult Delete(int docid, string returnURL)
        {
            //先要判断当前用户是否有权限删除这篇文档

            return RedirectToAction(returnURL, "User");
        }


        [Authorize]
        public void CancelFileUploaded(string id, string fileName)
        {
            BLLDocument bllDoc = new BLLDocument();
            bllDoc.DeleteTempFile(id, fileName);
        }

        [Authorize]
        public JsonResult FileDetail(DocumentModel document)
        {
            BLLDocument bllDoc = new BLLDocument();
            if (Session["username"] != null)
            {
                return Json(bllDoc.AddDocument(document, (Int32)Session["username"], Server.MapPath("~/")));
            }
            return Json(false);
        }
    }
}
