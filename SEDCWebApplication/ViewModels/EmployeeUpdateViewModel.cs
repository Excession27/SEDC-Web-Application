using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDCWebApplication.BLL.Logic.Models;
using Microsoft.AspNetCore.Http;

namespace SEDCWebApplication.ViewModels
{
    public class EmployeeUpdateViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public RoleEnum Role { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Photo { get; set; }

        public string PageTitle { get; set; }
    }
}