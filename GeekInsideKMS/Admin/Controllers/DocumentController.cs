﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;
using Model.Models;

namespace Admin.Controllers
{
    public class DocumentController : Controller
    {
        public static readonly string REPO_ROOT = "D:\\geekinsidekms\\repository\\";

        BLLDocument bllDocument = new BLLDocument();

        [Authorize]
        public ActionResult Index()
        {
            List<DocumentModel> docList = new BLLDocument().getAllDocOrderByPubtime();
            ViewData["docList"] = docList;
            return View();
        }

        //批量删除
        [HttpPost]
        [Authorize]
        public ActionResult deleteMultiDocs(string[] selected_docs)
        {
            Boolean result = true;
            foreach (string checkbox in selected_docs)
            {
                if (!bllDocument.deleteDocumentById(Convert.ToInt32(checkbox)))
                {
                    result = false;
                }
            }
            if (result)
            {
                TempData["successMsg"] = "删除成功！";
                return RedirectToAction("Index", "Document");
            }
            else
            {
                TempData["errorMsg"] = "删除失败！";
                return RedirectToAction("Index", "Document");
            }
        }

        //删除单个文档
        public ActionResult Delete(int docid)
        {
            Boolean result = new BLLDocument().deleteDocumentById(docid);
            if (result)
            {
                TempData["successMsg"] = "删除成功！";
                return RedirectToAction("Index", "Document");
            }
            else
            {
                TempData["errorMsg"] = "删除失败！";
                return RedirectToAction("Index", "Document");
            }
        }

        //编辑单个文档
        public ActionResult Edit(int docid)
        {
            DocumentModel docModel = new BLLDocument().getDocumentById(docid);
            ViewData["docModel"] = docModel;
            return View();
        }

        //保存编辑的单个文档
        [HttpPost]
        [Authorize]
        public ActionResult doEdit()
        {
            //标签暂时还不能改
            DocumentModel docModel = new DocumentModel();
            docModel.Id = Convert.ToInt32(Request.Form["id"]);
            docModel.FileDisplayName = Request.Form["filedisplayname"];
            docModel.Description = Request.Form["description"];
            docModel.AuthLevel = Convert.ToInt32(Request.Form["auth_"]);
            if (bllDocument.updateDocument(docModel))
            {
                TempData["successMsg"] = "更新成功。";
            } 
            else
            {
                TempData["errorMsg"] = "更新失败。";
            }
            return RedirectToAction("Index", "Document");
        }

        [Authorize]
        public void getFilePath(string FileDownloadName)
        {
            BLLDocument bllDocument = new BLLDocument();
            int docid = bllDocument.getDocumentByFileDiskName(FileDownloadName).Id;
            string folderPath = new BLLFolder().GetFolderById(bllDocument.getDocumentById(docid).FolderId).PhysicalPath;
            string fileType = FileDownloadName.Split('.')[1];
            string filePath = "";
            HttpContext.Response.AddHeader("content-disposition",
                "attachment; filename=" + FileDownloadName);            

            if (fileType.Equals("flv"))
            {
                filePath = REPO_ROOT + folderPath + "\\" + FileDownloadName;
            }
            else {
                //filePath = "C:\\Users\\Margaret\\Documents\\Visual Studio 2010\\Projects\\GeekInsideKMS\\Index\\swf\\" + FileDownloadName.Split('.')[0] + ".swf";
                filePath = REPO_ROOT + "swf\\" + FileDownloadName.Split('.')[0] + ".swf";
            }
            
            HttpContext.Response.TransmitFile(filePath);
        }
    }
}
