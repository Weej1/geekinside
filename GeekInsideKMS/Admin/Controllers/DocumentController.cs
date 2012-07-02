using System;
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
        [Authorize]
        public ActionResult Index()
        {
            List<DocumentModel> docList = new BLLDocument().getAllDocOrderByPubtime();
            ViewData["docList"] = docList;
            return View();
        }

    }
}
