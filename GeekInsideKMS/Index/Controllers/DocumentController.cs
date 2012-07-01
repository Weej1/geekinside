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

        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
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
            document.PublisherName = FormsAuthentication.FormsCookieName;
            //bllDoc.AddDocument(document);
            return Json(true);
        }
    }
}
