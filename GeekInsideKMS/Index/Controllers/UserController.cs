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
            string empno = User.Identity.Name;
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = Convert.ToInt32(empno);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            ViewData["empModel"] = empModel;
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
            int employeeNumber = Convert.ToInt32(User.Identity.Name);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            UserEmployeeDetailModel empDetailModel = new BLLUserAccount().GetUserDetailByEmpNumber(employeeNumber);
            ViewData["empModel"] = empModel;
            ViewData["empDetailModel"] = empDetailModel;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult doProfile()
        {
            UserEmployeeDetailModel empDetailModel = new UserEmployeeDetailModel
            {
                Id = Convert.ToInt32(Request.Form["empDetailId"]),
                EmployeeNumber = Convert.ToInt32(User.Identity.Name),
                Name = Request.Form["name"],
                Email = Request.Form["email"],
                Phone = Request.Form["phone"]
            };
            //非空验证
            if (empDetailModel.Name == "" || empDetailModel.Email == "" || empDetailModel.Phone == "")
            {
                TempData["errorMsg"] = "请填写所有的字段。";
            }
            else if (new BLLUserAccount().UpdateUserDetailAccount(empDetailModel))
            {
                TempData["successMsg"] = "保存成功。";
            }
            else
            {
                TempData["errorMsg"] = "保存失败。";
            }
            return RedirectToAction("Profile", "User");
        }

        //我的未审核文档
        [Authorize]
        public ActionResult UnCheckedDocList()
        {
            string empno = User.Identity.Name;
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = Convert.ToInt32(empno);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            ViewData["empModel"] = empModel;
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
            string empno = User.Identity.Name;
            List<DocumentModel> docList = new List<DocumentModel>();
            int employeeNumber = Convert.ToInt32(empno);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            ViewData["empModel"] = empModel;
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
            string employeeNumber = User.Identity.Name;
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(Convert.ToInt32(employeeNumber));
            ViewData["empModel"] = empModel;
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
            string employeeNumber = User.Identity.Name;
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(Convert.ToInt32(employeeNumber));
            //这里要先判断是否已经收藏过
            if (new BLLFavorite().isFavorite(Convert.ToInt32(employeeNumber),docid))
            {
                TempData["errorMsg"] = "您已经收藏过此文档了。";
                return RedirectToAction(returnURL, "User");
            }
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

        //审核界面
        [Authorize]
        public ActionResult Checker()
        {
            int employeeNumber = Convert.ToInt32(User.Identity.Name);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            ViewData["empModel"] = empModel;
            if (empModel.IsChecker.Equals(false))
            {
                //非审核员
                return RedirectToAction("Index", "Index");
            }
            List<DocumentModel> docList = new List<DocumentModel>();
            docList = new BLLDocument().getAllToBeCheckedDoc(employeeNumber);//传入employeeNumber：自己不能审核自己的
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

        //我审核过的文档
        [Authorize]
        public ActionResult CheckedByMe()
        {
            int employeeNumber = Convert.ToInt32(User.Identity.Name);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            ViewData["empModel"] = empModel;
            BLLDocument bllDocument = new BLLDocument();
            UserEmployeeModel userEmployeeModel = new BLLUserAccount().GetUserByEmpNumber(employeeNumber);
            if (userEmployeeModel.IsChecker.Equals(false))
            {
                //非审核员
                return RedirectToAction("Index", "Index");
            }
            List<DocumentModel> docList = new List<DocumentModel>();
            docList = new BLLDocument().getHaveCheckedDocByCheckerNumber(employeeNumber);
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

        //部门经理中心首页
        //文件夹最多三级
        public ActionResult Manager()
        {
            int empNumber = Convert.ToInt32(User.Identity.Name);
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(empNumber);
            ViewData["empModel"] = empModel;
            BLLFolder bllFolder = new BLLFolder();
            ViewData["outsideFolderId"] = new BLLDepartment().GetDepartment(empModel.DepartmentId).FolderId;
            if (empModel.IsManager.Equals(false))
            {
                //非部门经理
                return RedirectToAction("Index", "Index");
            }
            IList<FolderModel> folderModelList = bllFolder.getAllFoldersByDepartmentId(empModel.DepartmentId);

            if (folderModelList.Count() == 0)
            {
                ViewData["folderModelList"] = "nodata";
            } 
            else
            {
                ViewData["folderModelList"] = folderModelList.ToList<FolderModel>();
            }
            
            return View();
        }

        //添加文件夹
        [Authorize]
        public ActionResult addFolder(int parentId)
        {
            int empNumber = Convert.ToInt32(User.Identity.Name);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(empNumber);
            ViewData["empModel"] = empModel;
            BLLFolder bllFolder = new BLLFolder();
            ViewData["parentFolderModel"] = bllFolder.GetFolderById(parentId);
            return View();
        }

        //添加文件夹
        [HttpPost]
        [Authorize]
        public ActionResult doAddFolder()
        {
            FolderModel folderModel = new FolderModel();
            folderModel.FolderName = Request.Form["FolderName"];
            folderModel.Description = Request.Form["Description"];
            folderModel.ParentFolderId = Convert.ToInt32(Request.Form["ParentFolderId"]);
            BLLFolder bllFolder = new BLLFolder();
            if (bllFolder.addFolder(folderModel))
            {
                TempData["successMsg"] = "添加成功。";
            }
            else
            {
                TempData["errorMsg"] = "添加失败。";
            }
            return RedirectToAction("Manager", "User");
        }

        //删除文件夹
        public ActionResult deleteFolder(int folderId)
        {
            BLLFolder bllFolder = new BLLFolder();
            if (bllFolder.deleteFolderById(folderId))
            {
                TempData["successMsg"] = "删除成功。";
            }
            else
            {
                TempData["errorMsg"] = "删除失败。";
            }
            return RedirectToAction("Manager", "User");
            return null;
        }
    }
}
