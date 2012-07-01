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
            string employeeNumber = Session["username"].ToString();
            DocumentModel docModel = new BLLDocument().getDocumentById(docid);
            if (employeeNumber == "" || docModel == null || docModel.PublisherNumber != Convert.ToInt32(employeeNumber))
            {
                TempData["errorMsg"] = "您无权编辑此文件，请重新登录。";
                return RedirectToAction(returnURL, "User");
            }
            ViewData["docModel"] = docModel;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult doEdit(DocumentModel docModel)
        {
            //这里没写完
            string employeeNumber = Session["username"].ToString();
            TempData["successMsg"] = "更新成功。";
            return RedirectToAction("/User/Workshop", "User");
        }

        //删除文档
        [Authorize]
        public ActionResult Delete(int docid, string returnURL)
        {
            //先要判断当前用户是否有权限删除这篇文档
            string employeeNumber = Session["username"].ToString();
            BLLDocument bllDocument = new BLLDocument();
            DocumentModel docModel = bllDocument.getDocumentById(docid);
            if (employeeNumber == "" || docModel == null || docModel.PublisherNumber != Convert.ToInt32(employeeNumber))
            {
                TempData["errorMsg"] = "您无权删除此文件，请重新登录。";
                return RedirectToAction(returnURL, "User");
            }
            if (bllDocument.deleteDocumentById(docid))
            {
                TempData["successMsg"] = "删除成功。";
            } 
            else
            {
                TempData["errorMsg"] = "删除失败。";
            }
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
