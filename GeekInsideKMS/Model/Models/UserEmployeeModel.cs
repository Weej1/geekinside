using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class UserEmployeeModel
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string Password { get; set; }
        public bool IsChecker { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }
}
