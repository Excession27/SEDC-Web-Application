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
        public int? Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public List<Order> Orders { get; set; }
        public string ImagePath { get; set; }
        public bool Test { get; set; }

    }
}
