using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;

namespace DAL
{
    public class DALUserAccount: IDALUserAccount
    {
        private UserEmployeeModel ConvertFromDB(UserEmployee dbUser)
        {
            if (dbUser == null) return null;
            return new UserEmployeeModel
            {
                Id = dbUser.Id,
                EmployeeNumber = dbUser.EmployeeNumber,
                Password = dbUser.Password,
                IsChecker = dbUser.IsChecker,
                IsAvailable = dbUser.IsAvailable,               
                LastLoginTime = dbUser.LastLoginTime,
                IsManager = dbUser.IsManager,
                DepartmentId = dbUser.DepartmentId
            };
        }

        private UserEmployee ConvertToDB(UserEmployeeModel userEmployeeModel)
        {
            if (userEmployeeModel == null) return null;
            DAL.UserEmployee userEmployee = new DAL.UserEmployee();
            userEmployee.EmployeeNumber = userEmployeeModel.EmployeeNumber;
            userEmployee.Password = userEmployeeModel.Password;
            userEmployee.IsChecker = userEmployeeModel.IsChecker;
            userEmployee.IsAvailable = userEmployeeModel.IsAvailable;
            userEmployee.LastLoginTime = userEmployeeModel.LastLoginTime;
            userEmployee.DepartmentId = userEmployeeModel.DepartmentId;
            userEmployee.IsManager = userEmployeeModel.IsManager;

            return userEmployee; 

        }

        private UserEmployeeDetail ConvertToDBDetail(UserEmployeeDetailModel userEmployeeDetailModel)
        {
            if (userEmployeeDetailModel == null) return null;
            DAL.UserEmployeeDetail userEmployeeDetail = new DAL.UserEmployeeDetail();
            userEmployeeDetail.EmployeeNumber = userEmployeeDetailModel.EmployeeNumber;
            userEmployeeDetail.Name = userEmployeeDetailModel.Name;
            userEmployeeDetail.Email = userEmployeeDetailModel.Email;
            userEmployeeDetail.Phone = userEmployeeDetailModel.Phone;

            return userEmployeeDetail;

        }

        public UserEmployeeModel getUserByEmployeeNumber(int employeeNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                UserEmployee dbUser = (from u in gikms.UserEmployees
                                       where u.EmployeeNumber.Equals(employeeNumber)
                                       select u).FirstOrDefault();
                return ConvertFromDB(dbUser);
            }
        }

        public Boolean CreateUserAccount(UserEmployeeModel userEmployeeModel, UserEmployeeDetailModel userEmployeeDetail) 
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.UserEmployee userEmployee = ConvertToDB(userEmployeeModel);
            context.AddToUserEmployees(userEmployee);

            DAL.UserEmployeeDetail userDetail = ConvertToDBDetail(userEmployeeDetail);
            context.AddToUserEmployeeDetails(userDetail);

            context.SaveChanges();
            return true;
        }

        public Boolean UpdateUserAccount(UserEmployeeModel userEmployeeModel) 
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            UserEmployeeDetail empDetal = (from d in context.UserEmployeeDetails
                                           where d.EmployeeNumber == userEmployeeModel.EmployeeNumber
                                           select d).FirstOrDefault();

            empDetal.EmployeeNumber = userEmployeeModel.EmployeeNumber;
            empDetal.Name = userEmployeeModel.Name;
            empDetal.Email = userEmployeeModel.Email;
            empDetal.Phone = userEmployeeModel.Phone;

            context.SaveChanges();

            UserEmployee emp = (from u in context.UserEmployees
                       where u.EmployeeNumber == userEmployeeModel.EmployeeNumber 
                       select u).FirstOrDefault();

            emp.EmployeeNumber = userEmployeeModel.EmployeeNumber;
            emp.Password = userEmployeeModel.Password;
            emp.DepartmentId = userEmployeeModel.DepartmentId;
            emp.IsManager = userEmployeeModel.IsManager;
            emp.IsAvailable = userEmployeeModel.IsAvailable;
            emp.IsChecker = userEmployeeModel.IsChecker;
            emp.LastLoginTime = userEmployeeModel.LastLoginTime;

            context.SaveChanges();
            
            return true;
        }

        public Boolean DeleteUserAccount(UserEmployeeModel userEmployeeModel, UserEmployeeDetailModel userEmployeeDetailModel)  
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            UserEmployeeDetail dbDetail = (from detail in context.UserEmployeeDetails
                                           where detail.EmployeeNumber == userEmployeeDetailModel.EmployeeNumber
                                           select detail).FirstOrDefault();
            context.DeleteObject(dbDetail);

            context.SaveChanges();

            UserEmployee dbUser = (from user in context.UserEmployees
                                   where user.EmployeeNumber == userEmployeeModel.EmployeeNumber
                                   select user).FirstOrDefault();
            context.DeleteObject(dbUser);
            
            context.SaveChanges();
            return true;
        }

        public UserEmployeeDetailModel GetEmployeeDetailByEmployeeNumber(int employeeNumber)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            UserEmployeeDetail dbDetail = (from detail in context.UserEmployeeDetails
                                           where detail.EmployeeNumber == employeeNumber
                                           select detail).FirstOrDefault();

            UserEmployeeDetailModel empDetail = new UserEmployeeDetailModel();
            empDetail.Id = dbDetail.Id;
            empDetail.EmployeeNumber = dbDetail.EmployeeNumber;
            empDetail.Name = dbDetail.Name;
            empDetail.Email = dbDetail.Email;
            empDetail.Phone = dbDetail.Phone;

            return empDetail;
        }

        public int GetMaxEmployeeNumber() 
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            var allEmp = from emp in context.UserEmployees select emp;

            int maxEmpNumber = (from max in allEmp select max.EmployeeNumber).Max();

            return maxEmpNumber;
        }

        public Boolean UpdateUserDetailAccount(UserEmployeeDetailModel userEmployeeDetail)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            UserEmployeeDetail empDetal = (from d in context.UserEmployeeDetails
                                           where d.Id == userEmployeeDetail.Id
                                           select d).FirstOrDefault();

            empDetal.EmployeeNumber = userEmployeeDetail.EmployeeNumber;
            empDetal.Name = userEmployeeDetail.Name;
            empDetal.Email = userEmployeeDetail.Email;
            empDetal.Phone = userEmployeeDetail.Phone;

            context.SaveChanges();
            return true;
        }
    }
}
