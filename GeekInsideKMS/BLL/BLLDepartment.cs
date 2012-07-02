using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using Utils;

namespace BLL
{
    public class BLLDepartment
    {
        IDAL.IDALDepartment departmentDAL = DALFactory.DataAccess.CreateDepartmentDAL();
        IDAL.IDALFolder folderDAL = DALFactory.DataAccess.CreateFolderDAL();
        
        // 添加部门
        public void CreateDepartment(string deptName, string folderDesc)
        {
            FolderModel deptFolder = new FolderModel
            {
                FolderName = deptName,
                Description = folderDesc,
                ParentFolderId = 0, // 1级目录
                PhysicalPath = Helper.CreateNewFolderPath(@"\") // 1级目录存放在根目录下
            };
            Helper.CreateDirectory(deptFolder.PhysicalPath);    // 创建目录

            int folderId = folderDAL.CreateFolder(deptFolder);

            DepartmentModel dept = new DepartmentModel
            {
                DepartmentName = deptName,
                FolderId = folderId
            };
            departmentDAL.CreateDepartment(dept);
        }

        // 查询部门
        public DepartmentModel GetDepartment(int dept_id)
        {
            return departmentDAL.GetDepartmentById(dept_id);
        }

        // 返回所有部门
        public IList<DepartmentModel> GetAllDepartments()
        {
            return departmentDAL.GetAllDepartments();
        }

        // 更新部门信息
        public void UpdateDepartment(DepartmentModel dept)
        {
            departmentDAL.UpdateDepartment(dept);
        }

    }
}
