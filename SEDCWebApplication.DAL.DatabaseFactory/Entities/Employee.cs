using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class Employee : User
    {

        public string EmployeeName { get; set; }

        public int RoleId { get; set; }

        public string Pol { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
