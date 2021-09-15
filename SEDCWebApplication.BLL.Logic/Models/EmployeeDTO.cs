using SEDCWebApplication.DAL.EntityFactory.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Models
{
    public class EmployeeDTO
    {
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        public string EmployeeName { get; set; }
        public RoleEnum Role { get; set; }
        public List<Order> Orders { get; set; }
        public string Pol { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public bool Test { get; set; }
    }
}
