using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Models;
using Utils;
using Admin.Models;

namespace Admin.Controllers
{
    public class DepartmentController : Controller
    {
        BLLDepartment departmentBL = new BLLDepartment();
        BLLFolder folderBL = new BLLFolder();
        //
        // GET: /Department/
        [Authorize]
        public ActionResult Index()
        {
            IList<DepartmentModel> depts = departmentBL.GetAllDepartments();
            var viewRows = from d in depts
                        select new DepartmentRow
                        {
                            Id = d.Id,
                            DepartmentName = d.DepartmentName,
                            FolderPath = Helper.REPO_ROOT + folderBL.GetFolderById(d.FolderId).PhysicalPath
                        };
            return View(viewRows.ToArray());
        }

        //
        // GET: /Department/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Department/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string deptName = Request.Form["deptName"];
                string folderDesc = Request.Form["folderDesc"];
                departmentBL.CreateDepartment(deptName, folderDesc);
                TempData["successMsg"] = "添加成功";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Department/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            DepartmentModel dept = departmentBL.GetDepartment(id);
            FolderModel folder = folderBL.GetFolderById(dept.FolderId);
            ViewData["id"] = dept.Id;
            ViewData["deptName"] = dept.DepartmentName;
            ViewData["folderDesc"] = folder.Description;
            return View();
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string deptName = Request.Form["deptName"];
                string desc = Request.Form["folderDesc"];

                DepartmentModel dept = departmentBL.GetDepartment(id);
                dept.DepartmentName = deptName;
                FolderModel folder = folderBL.GetFolderById(dept.FolderId);
                folder.FolderName = deptName;
                folder.Description = desc;

                // 更新
                departmentBL.UpdateDepartment(dept);
                folderBL.UpdateFolder(folder);

                TempData["successMsg"] = "修改成功";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
