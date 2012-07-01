using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALDepartment
    {
        /**
         * 创建新的部门
         * 返回新建部门的ID
         */ 
        int CreateDepartment(DepartmentModel department);

        /**
         * 更新部门信息（不包括部门关联的文件夹信息）
         */ 
        void UpdateDepartment(DepartmentModel department);

        /**
         * 删除部门（值删除部门本身，不包括关联的文件夹）
         */ 
        void DeleteDepartment(DepartmentModel department);

        /**
         * 得到所有的部门
         */ 
        IList<DepartmentModel> GetAllDepartments();

        /**
         * 根据部门ID得到部门
         */ 
        DepartmentModel GetDepartmentById(int dept_id);
    }
}
