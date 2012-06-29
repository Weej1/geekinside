using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class UserAdminModel
    {
        public UserAdminModel()
        {

        }

        public UserAdminModel(int Id, int EmployeeNumber, string Username, string Password, DateTime LastLoginTime)
        {
            this.Id = Id;
            this.EmployeeNumber = EmployeeNumber;
            this.Username = Username;
            this.Password = Password;
            this.LastLoginTime = LastLoginTime;
        }

        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
