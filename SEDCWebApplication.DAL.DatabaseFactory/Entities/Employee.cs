using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class Employee : User
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int RoleId { get; set; }
        public string Pol { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ImagePath { get; set; }

        //public User EmployeeNavigation { get; set; }
        //public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
