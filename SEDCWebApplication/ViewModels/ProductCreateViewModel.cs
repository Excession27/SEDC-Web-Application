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
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno")]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsDiscounted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Size { get; set; }
        public IFormFile Photo { get; set; }
    }
}
