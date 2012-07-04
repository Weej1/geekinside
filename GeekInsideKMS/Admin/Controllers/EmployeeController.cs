using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model.Models;
using BLL;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Transactions;

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
        public ActionResult doCreateUser()
        {
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            UserEmployeeModel userEmployeeModel = new UserEmployeeModel();
            UserEmployeeDetailModel userEmployeeDetailModel = new UserEmployeeDetailModel();
            
            userEmployeeModel.EmployeeNumber = bllUserAccount.GetMaxEmployeeNumber() + 1;           
            userEmployeeModel.Password = "123456";
            userEmployeeModel.DepartmentId = Convert.ToInt32(Request.Form["dept_name"]);
            userEmployeeModel.IsManager = (Convert.ToInt32(Request.Form["isManager"]) == 0 ? false : true);
            userEmployeeModel.IsChecker = (Convert.ToInt32(Request.Form["isChecker"]) == 0 ? false : true);
            userEmployeeModel.IsAvailable = (Convert.ToInt32(Request.Form["isAvailable"]) == 0 ? false : true);

            userEmployeeModel.Name = Request.Form["name"];
            userEmployeeModel.Email = Request.Form["email"];
            userEmployeeModel.Phone = Request.Form["phone"];

            if (userEmployeeModel.Email == null)
            {
                TempData["employeeNumberErrorMsg"] = "请输入正确的邮箱地址！";
                return RedirectToAction("CreateUser", "Employee");
            }

            if (userEmployeeModel.Phone == null)
            {
                TempData["phoneErrorMsg"] = "请输入正确的手机号！";
                return RedirectToAction("CreateUser", "Employee");
            }

            Boolean result = bllUserAccount.CreateUserAccount(userEmployeeModel);

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

            if (userEmployeeModel.Email == "")
            {
                TempData["employeeNumberErrorMsg"] = "请输入正确的邮箱地址！";
                return RedirectToAction("Edit", "Employee", userEmployeeModel.EmployeeNumber);
            }

            if (userEmployeeModel.Phone == "")
            {
                TempData["phoneErrorMsg"] = "请输入正确的手机号！";
                return RedirectToAction("Edit", "Employee", userEmployeeModel.EmployeeNumber);
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

        //批量删除员工
        [HttpPost]
        [Authorize]
        [MultiButton("deleteMultiEmps")]
        public ActionResult deleteMultiEmps(string[] selected_emps)
        {
            Boolean result = true;
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            foreach (string checkbox in selected_emps)
            {
                if (!bllUserAccount.DeleteUserAccount(bllUserAccount.GetUserByEmpNumber(Convert.ToInt32(checkbox)), bllUserAccount.GetUserDetailByEmpNumber(Convert.ToInt32(checkbox))))
                {
                    result = false;
                }
            }
            if (result)
            {
                TempData["successMsg"] = "删除成功！";
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                TempData["errorMsg"] = "删除失败！";
                return RedirectToAction("Index", "Employee");
            }
        }

        //批量导入
        [Authorize]
        public ActionResult StationImport()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult StationImport(HttpPostedFileBase filebase)
        {
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;

            if (file == null || file.ContentLength <= 0)
            {
                ViewData["errorMsg"] = "文件不能为空";
                return View();
            }

            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx,.cvs";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewData["errorMsg"] = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewData["errorMsg"] = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\uploads\\excel\\";
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }
            //string result = string.Empty;
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet, "ExcelInfo");
            }
            catch (Exception ex)
            {
                ViewData["errorMsg"] = ex.Message;
                return View();
            }
            DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();

            //引用事务机制，出错时，事物回滚
            using (TransactionScope transaction = new TransactionScope())
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    UserEmployeeModel temp = new UserEmployeeModel();
                    temp.EmployeeNumber = bllUserAccount.GetMaxEmployeeNumber() + 1;
                    temp.Password = "123456";
                    temp.Name = table.Rows[i].ItemArray[0].ToString();
                    temp.Email = table.Rows[i].ItemArray[1].ToString();
                    temp.Phone = table.Rows[i].ItemArray[2].ToString();
                    temp.DepartmentId = Convert.ToInt32(table.Rows[i].ItemArray[3].ToString());
                    temp.IsManager = (Convert.ToInt32(table.Rows[i].ItemArray[4].ToString()) == 0 ? false : true);
                    temp.IsChecker = (Convert.ToInt32(table.Rows[i].ItemArray[5].ToString()) == 0 ? false : true);
                    temp.IsAvailable = (Convert.ToInt32(table.Rows[i].ItemArray[6].ToString()) == 0 ? false : true);
                    bllUserAccount.CreateUserAccount(temp);
                }
                transaction.Complete();
            }
            ViewData["successMsg"] = "导入成功";
            System.Threading.Thread.Sleep(2000);
            return RedirectToAction("Index");

        }

        [Authorize]
        [HttpPost]
        [MultiButton("Search")]
        public ActionResult Search() 
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

            int deptId = Convert.ToInt32(Request.Form["dept_name"]);
            string conditions = Request.Form["search_key"];
            BLLUserAccount bllUserAccount = new BLLUserAccount();
            List<UserEmployeeModel> empList = new List<UserEmployeeModel>();
            List<UserEmployeeModel> resultList = new List<UserEmployeeModel>(); 

            if (deptId == 0)
            {
                empList = bllUserAccount.GetAllEmployeeDetails();
            }
            else 
            {
                empList = bllUserAccount.GetUserDetailsByDeptId(deptId);
            }
            if (conditions.Equals("登录名/姓名"))
            {
                ViewData["empList"] = empList;
                return View();
            }
            else 
            {
                if (isnumeric(conditions))
                {
                    foreach (UserEmployeeModel element in empList)
                    {
                        if (element.EmployeeNumber == Convert.ToInt32(conditions))
                            resultList.Add(element);
                    }
                    ViewData["empList"] = resultList;
                }
                else 
                {
                    foreach (UserEmployeeModel element in empList)
                    {
                        if (element.Name == conditions)
                            resultList.Add(element);
                    }
                    ViewData["empList"] = resultList;
                }
                return View();
            }
            
        }

        private bool isnumeric(string str)
        {
            char[] ch=new char[str.Length];
            ch=str.ToCharArray();
            foreach (char element in ch)
            {
                if (element < 48 || element > 57)
                return false;
            }
            return true;
        }
       
        public class MultiButtonAttribute : ActionNameSelectorAttribute 
        { 
            public string Name { get; set; } 
            public MultiButtonAttribute(string name) 
            { 
                this.Name = name; 
            } 
            public override bool IsValidName(ControllerContext controllerContext, 
            string actionName, System.Reflection.MethodInfo methodInfo) 
            { 
                if (string.IsNullOrEmpty(this.Name)) 
                { 
                    return false; 
                } 
                return controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name); 
            } 
        }
        
        [Authorize]
        [HttpPost]
        [MultiButton("Export")]
        public void ExportMsg()
        {
            List <UserEmployeeModel> emps = new BLLUserAccount().GetAllEmployeeDetails();
            DataTable dt = new DataTable();
            string strFileName;
            int cloumns = 8;

            //生成文件名: 当前年月日小时分钟秒+ 随机数
            Random rd = new Random(int.Parse(DateTime.Now.ToString("MMddhhmmss")));
            strFileName = DateTime.Now.ToString("yyyyMMdd")
                + DateTime.Now.Hour
                + DateTime.Now.Minute
                + DateTime.Now.Second
                + rd.Next(999999).ToString()
                + ".csv";
            StringWriter sw = new StringWriter();

            string title = string.Empty;
            string content = string.Empty;
            string[] titlearray = { "员工号", "姓名", "email", "手机号", "部门id", "是否为经理", "是否为审核员", "是否可用" };

            for(int i = 0; i< titlearray.Count(); i++ ) 
            {
                if (i == 0)
                    title = title + titlearray[i];
                else
                    title = title + "," +titlearray[i];
            }
            sw.WriteLine(title);
            for (int i = 0; i < emps.Count(); i++)
            {
                content = content + emps[i].EmployeeNumber.ToString();
                content = content + "," + emps[i].Name;
                content = content + "," + emps[i].Email;
                content = content + "," + emps[i].Phone;
                content = content + "," + emps[i].DepartmentId.ToString();
                content = content + "," + (emps[i].IsManager == true ? 1 : 0).ToString();
                content = content + "," + (emps[i].IsChecker == true ? 1 : 0).ToString();
                content = content + "," + (emps[i].IsAvailable == true ? 1 : 0).ToString();
                sw.WriteLine(content);
                content = string.Empty;
            }

            sw.Close();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(strFileName));

            Response.ContentType = "vnd.ms-excel.numberformat:yyyy-MM-dd ";

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            Response.Write(sw);

            Response.End();
        }
    }
}
