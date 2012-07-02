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

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Document/Detail
        [Authorize]
        public ActionResult Detail(int docid)
        {
            DocumentModel docModel = new BLLDocument().getDocumentById(docid);
            ViewData["docModel"] = docModel;
            List<DocumentModel> viewdocList = new BLLDocument().getTopTenDocumentByViewNumber();
            ViewData["viewTop10Doc"] = viewdocList;
            List<DocumentModel> dldocList = new BLLDocument().getTopTenDocumentByDownloadNumber();
            ViewData["dlTop10Doc"] = dldocList;
            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {
            string employeeNumber = User.Identity.Name;
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(Convert.ToInt32(employeeNumber));
            ViewData["empModel"] = empModel;
            return View("DocumentUpload");
        }

        //编辑文档信息
        [Authorize]
        public ActionResult Edit(int docid,string returnURL)
        {
            //先要判断当前用户是否有权限编辑这篇文档（是不是发布者）
            string employeeNumber = User.Identity.Name;
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
        public ActionResult doEdit()
        {
            //标签暂时还不能改
            DocumentModel docModel = new DocumentModel();
            docModel.Id = Convert.ToInt32(Request.Form["id"]);
            docModel.FileDisplayName = Request.Form["filedisplayname"];
            docModel.Description = Request.Form["description"];
            if (new BLLDocument().updateDocument(docModel))
            {
                TempData["successMsg"] = "更新成功。";
            }
            else
            {
                TempData["errorMsg"] = "更新失败。";
            }
            return RedirectToAction("Workshop", "User");
        }

        //删除文档
        [Authorize]
        public ActionResult Delete(int docid, string returnURL)
        {
            //先要判断当前用户是否有权限删除这篇文档
            string employeeNumber = User.Identity.Name;
            BLLDocument bllDocument = new BLLDocument();
            DocumentModel docModel = bllDocument.getDocumentById(docid);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(Convert.ToInt32(employeeNumber));
            //如果是审核者，则直接删除
            if (empModel != null && empModel.IsChecker.Equals(true) && docModel.CheckerNumber.Equals(Convert.ToInt32(employeeNumber)))
            {
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
            //如果不是审核者，也不是这篇文档的发布者，删除失败
            if (employeeNumber == "" || docModel == null || docModel.PublisherNumber != Convert.ToInt32(employeeNumber))
            {
                TempData["errorMsg"] = "您无权删除此文件，请重新登录。";
                return RedirectToAction(returnURL, "User");
            }
            //如果不是审核者，也是这篇文档的发布者，则删除
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

        //将文档设置为已审核
        [Authorize]
        public ActionResult doCheck(int docid, string returnURL)
        {
            //先要判断当前用户是否有权设置这篇文档
            string employeeNumber = User.Identity.Name;
            BLLDocument bllDocument = new BLLDocument();
            DocumentModel docModel = bllDocument.getDocumentById(docid);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(Convert.ToInt32(employeeNumber));
            //如果不是审核者或不是这篇文档的审核者，则返回错误
            if (empModel == null || empModel.IsChecker.Equals(false) || !docModel.CheckerNumber.Equals(Convert.ToInt32(employeeNumber)))
            {
                TempData["errorMsg"] = "您无权操作此文件，请重新登录。";
                return RedirectToAction(returnURL, "User");
            }
            //如果是审核者,可以操作
            if (bllDocument.setDocCheckedById(docid))
            {
                TempData["successMsg"] = "操作成功。";
            }
            else
            {
                TempData["errorMsg"] = "操作失败。";
            }
            return RedirectToAction(returnURL, "User");
        }

        //将我审核通过的文档重新设置为未审核
        [Authorize]
        public ActionResult doUnCheck(int docid, string returnURL)
        {
            //先要判断当前用户是否有权设置这篇文档
            string employeeNumber = User.Identity.Name;
            BLLDocument bllDocument = new BLLDocument();
            DocumentModel docModel = bllDocument.getDocumentById(docid);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(Convert.ToInt32(employeeNumber));
            //如果不是审核者或不是这篇文档的审核者，则返回错误
            if (empModel == null || empModel.IsChecker.Equals(false) || !docModel.CheckerNumber.Equals(Convert.ToInt32(employeeNumber)))
            {
                TempData["errorMsg"] = "您无权操作此文件，请重新登录。";
                return RedirectToAction(returnURL, "User");
            }
            //如果是审核者,可以操作
            if (bllDocument.setDocUncheckedById(docid))
            {
                TempData["successMsg"] = "操作成功。";
            }
            else
            {
                TempData["errorMsg"] = "操作失败。";
            }
            return RedirectToAction(returnURL, "User");
        }

        //显示某人的所以所有发布的文档
        public ActionResult GetDocByEmpployeeNumber(int empno)
        {
            List<DocumentModel> docList = new BLLDocument().GetDocByEmpployeeNumber(empno);
            UserEmployeeDetailModel empDetailModel = new BLLUserAccount().GetUserDetailByEmpNumber(empno);
            ViewData["empDetailModel"] = empDetailModel;
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
    }
}
