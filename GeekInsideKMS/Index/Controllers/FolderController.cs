using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Models;

namespace Index.Controllers
{
    public class FolderController : Controller
    {
        //
        // GET: /Folder/

        public JsonResult AllFolders()
        {
            BLLFolder bllFolder = new BLLFolder();
            List<FolderModel> folders = (List<FolderModel>)bllFolder.GetAllFolders();
            return Json(folders);
        }

    }
}
