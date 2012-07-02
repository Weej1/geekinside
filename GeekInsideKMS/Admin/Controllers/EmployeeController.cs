using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model.Models;
using BLL;
using System.Text.RegularExpressions;

namespace Admin.Controllers
{
    public class EmployeeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            IList<DepartmentModel> deptList = new List<DepartmentModel>();
            BLLDepartment bllDepartment = new BLLDepartment();
            deptList = bllDepartment.GetAllDepartments();
            if (deptList.Count == 0)
            {
                ViewData["deptList"] = "nodata";
            }
            else
            {
                ViewData["deptList"] = deptList;
            }

            List<UserEmployeeModel> empList = new List<UserEmployeeModel>();
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            empList = bllUserAccount.GetAllEmployeeDetails();
            if (empList.Count == 0) 
            {
                ViewData["empList"] = "nodata";
            }
            else
            {
                ViewData["empList"] = empList;
            }

            return View();
        }

        [Authorize]
        public ActionResult CreateUser()
        {
            ViewData["employeeNumber"] = new BLLUserAccount().GetMaxEmployeeNumber() + 1;

            IList<DepartmentModel> deptList = new List<DepartmentModel>();
            BLLDepartment bllDepartment = new BLLDepartment();
            deptList = bllDepartment.GetAllDepartments();
            if (deptList.Count == 0)
            {
                ViewData["deptList"] = "nodata";
            }
            else
            {
                ViewData["deptList"] = deptList;
            }
            return View();
        }

        [HttpPost]
        public ActionResult FilterUsers()
        {
            int deptId = Convert.ToInt32(Request.Form["dept_name"]);
            List<UserEmployeeModel> empList = new List<UserEmployeeModel>();
            BLLUserAccount bllUserAccount = new BLLUserAccount();

            if (deptId == 0)
            {
                empList = bllUserAccount.GetAllEmployeeDetails();
            }
            else 
            {
                empList = bllUserAccount.GetEmployeeDetailsByDept(deptId);
            }
            ViewData["empList"] = empList;

            return View();
        }

        [HttpPost]
        public ActionResult doCreateUser() 
        {
            string REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)(\.\w+))*\@\w+((\.-)\w+)*\.\w+$";  //电子邮件校验常量

            BLLUserAccount bllUserAccount = new BLLUserAccount();
            UserEmployeeModel userEmployeeModel = new UserEmployeeModel();
            UserEmployeeDetailModel userEmployeeDetailModel = new UserEmployeeDetailModel();

            userEmployeeModel.EmployeeNumber = bllUserAccount.GetMaxEmployeeNumber() + 1;            
            userEmployeeModel.Password = "123456";
            userEmployeeModel.DepartmentId = Convert.ToInt32(Request.Form["dept_name"]);
            userEmployeeModel.IsManager = (Convert.ToInt32(Request.Form["isManager"])==0 ? false : true);
            userEmployeeModel.IsChecker = (Convert.ToInt32(Request.Form["isChecker"])==0 ? false : true);
            userEmployeeModel.IsAvailable = (Convert.ToInt32(Request.Form["isAvailable"])==0 ? false : true);

            userEmployeeDetailModel.EmployeeNumber = Convert.ToInt32(Request.Form["employeeNumber"]);
            userEmployeeDetailModel.Name = Request.Form["name"];
            userEmployeeDetailModel.Email = Request.Form["email"];
            userEmployeeDetailModel.Phone = Request.Form["phone"];

            if (userEmployeeDetailModel.Email == null || !Regex.IsMatch(userEmployeeDetailModel.Email, REGEXP_IS_VALID_EMAIL)) 
            {
                TempData["employeeNumberErrorMsg"] = "请输入正确的邮箱地址！";
                return RedirectToAction("CreateUser", "Employee");
            }

            if (userEmployeeDetailModel.Phone == null )
            {
                TempData["phoneErrorMsg"] = "请输入正确的手机号！";
                return RedirectToAction("CreateUser", "Employee");
            }

            Boolean result = bllUserAccount.CreateUserAccount(userEmployeeModel, userEmployeeDetailModel);
            
            if (result == true)
            {
                ViewData["successMsg"] = "添加成功";
                return RedirectToAction("Index", "Employee");
            }
            else 
            {
                ViewData["errorMsg"] = "添加失败";
                return RedirectToAction("Index", "Employee");
            }
        }

        [Authorize]
        public ActionResult Delete(int empNo)
        {
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            bllUserAccount.GetUserByEmpNumber(empNo);
            if (bllUserAccount.DeleteUserAccount(bllUserAccount.GetUserByEmpNumber(empNo), 
                bllUserAccount.GetUserDetailByEmpNumber(empNo)))
            {
                TempData["successMsg"] = "删除成功。";
            }
            else
            {
                TempData["errorMsg"] = "删除失败。";
            }
            return RedirectToAction("Index", "Employee");
        }

        [Authorize]
        public ActionResult Edit(int empNo) 
        {
            IList<DepartmentModel> deptList = new List<DepartmentModel>();
            BLLDepartment bllDepartment = new BLLDepartment();
            deptList = bllDepartment.GetAllDepartments();
            if (deptList.Count == 0)
            {
                ViewData["deptList"] = "nodata";
            }
            else
            {
                ViewData["deptList"] = deptList;
            }

            BLLUserAccount bllUserAccount = new BLLUserAccount();

            UserEmployeeModel userEmployeeModel = bllUserAccount.GetSingleUser(empNo);

            ViewData["id"] = userEmployeeModel.Id;
            ViewData["employeeNumber"] = userEmployeeModel.EmployeeNumber;
            ViewData["password"] = userEmployeeModel.Password;
            ViewData["dept_name"] = userEmployeeModel.DepartmentId;
            ViewData["isManager"] = userEmployeeModel.IsManager;
            ViewData["isChecker"] = userEmployeeModel.IsChecker;
            ViewData["isAvailable"] = userEmployeeModel.IsChecker;
            ViewData["name"] = userEmployeeModel.Name;
            ViewData["email"] = userEmployeeModel.Email;
            ViewData["phone"] = userEmployeeModel.Phone;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult doEdit()
        {
            string REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)(\.\w+))*\@\w+((\.-)\w+)*\.\w+$";  //电子邮件校验常量

            BLLUserAccount bllUserAccount = new BLLUserAccount();

            int id = Convert.ToInt32(Request.Form["id"]);

            UserEmployeeModel userEmployeeModel = bllUserAccount.GetUserByEmpNumber(Convert.ToInt32(Request.Form["empno"]));

            if (Convert.ToString(Request.Form["password"]) != null) 
            {
                userEmployeeModel.Password = Convert.ToString(Request.Form["password"]);
            }
            userEmployeeModel.DepartmentId = Convert.ToInt32(Request.Form["dept_name"]);
            userEmployeeModel.IsManager = (Convert.ToInt32(Request.Form["isManager"]) == 0 ? false : true);
            userEmployeeModel.IsChecker = (Convert.ToInt32(Request.Form["isChecker"]) == 0 ? false : true);
            userEmployeeModel.IsAvailable = (Convert.ToInt32(Request.Form["isAvailable"]) == 0 ? false : true);

            userEmployeeModel.EmployeeNumber = Convert.ToInt32(Request.Form["empno"]);
            userEmployeeModel.Name = Request.Form["name"];
            userEmployeeModel.Email = Request.Form["email"];
            userEmployeeModel.Phone = Request.Form["phone"];

            if (userEmployeeModel.Email == null || !Regex.IsMatch(userEmployeeModel.Email, REGEXP_IS_VALID_EMAIL))
            {
                TempData["employeeNumberErrorMsg"] = "请输入正确的邮箱地址！";
                return RedirectToAction("CreateUser", "Employee");
            }

            if (userEmployeeModel.Phone == null)
            {
                TempData["phoneErrorMsg"] = "请输入正确的手机号！";
                return RedirectToAction("CreateUser", "Employee");
            }

            Boolean result = bllUserAccount.UpdateUserAccount(userEmployeeModel);

            if (result == true)
            {
                ViewData["successMsg"] = "修改成功";
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewData["errorMsg"] = "修改失败";
                return RedirectToAction("Index", "Employee");
            }
        }
    }
}
