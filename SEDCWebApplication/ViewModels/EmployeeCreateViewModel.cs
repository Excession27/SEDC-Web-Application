using Microsoft.AspNetCore.Http;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno")]
        public string Name { get; set; }

        public string Email { get; set; }
        public RoleEnum Role { get; set; }

        public IFormFile Photo { get; set; }
    }
}
