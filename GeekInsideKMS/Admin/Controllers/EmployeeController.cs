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
            return View();
        }

        [Authorize]
        public ActionResult CreateUser()
        {
            ViewData["employeeNumber"] = new BLLUserAccount().GetMaxEmployeeNumber() + 1;
            return View();
        }

        [HttpPost]
        public ActionResult doCreateUser() 
        {
            string REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)(\.\w+))*\@\w+((\.-)\w+)*\.\w+$";  //电子邮件校验常量
            string REGEXP_IS_VALID_PHONE = @"/^\d{7,8}$/"; //手机号校验常量

            BLLUserAccount bllUserAccount = new BLLUserAccount();
            UserEmployeeModel userEmployeeModel = new UserEmployeeModel();
            UserEmployeeDetailModel userEmployeeDetailModel = new UserEmployeeDetailModel();

            userEmployeeModel.EmployeeNumber = bllUserAccount.GetMaxEmployeeNumber() + 1;            
            userEmployeeModel.Password = "123456";
            userEmployeeModel.DepartmentId = Convert.ToInt16(Request.Form["departmentId"]);
            userEmployeeModel.IsManager = (Convert.ToInt16(Request.Form["isManager"])==0 ? false : true);
            userEmployeeModel.IsChecker = (Convert.ToInt16(Request.Form["isChecker"])==0 ? false : true);
            userEmployeeModel.IsAvailable = (Convert.ToInt16(Request.Form["isAvailable"])==0 ? false : true);

            userEmployeeDetailModel.EmployeeNumber = Convert.ToInt16(Request.Form["employeeNumber"]);
            userEmployeeDetailModel.Name = Request.Form["name"];
            userEmployeeDetailModel.Email = Request.Form["email"];
            userEmployeeDetailModel.Phone = Request.Form["phone"];

            if (userEmployeeDetailModel.Email == null || !Regex.IsMatch(userEmployeeDetailModel.Email, REGEXP_IS_VALID_EMAIL)) 
            {
                TempData["employeeNumberErrorMsg"] = "请输入正确的邮箱地址！";
                return RedirectToAction("CreateUser", "Employee");
            }

            bool phoneResult = Regex.IsMatch(userEmployeeDetailModel.Phone, REGEXP_IS_VALID_EMAIL);
            if (userEmployeeDetailModel.Phone == null || !phoneResult)
            {
                TempData["phoneErrorMsg"] = "请输入正确的手机号！";
                return RedirectToAction("CreateUser", "Employee");
            }

            Boolean result = bllUserAccount.CreateUserAccount(userEmployeeModel, userEmployeeDetailModel);
            
            if (result == true)
            {
                ViewData["successMsg"] = "添加成功";
                return RedirectToAction("CreateUser", "Employee");
            }
            else 
            {
                ViewData["errorMsg"] = "添加失败";
                return RedirectToAction("CreateUser", "Employee");
            }
        }

    }
}
