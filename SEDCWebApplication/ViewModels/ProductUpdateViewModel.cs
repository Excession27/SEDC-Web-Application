using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDCWebApplication.BLL.Logic.Models;
using Microsoft.AspNetCore.Http;

namespace SEDCWebApplication.ViewModels
{
    public class ProductUpdateViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsDiscounted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Size { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Photo { get; set; }

        public string PageTitle { get; set; }
    }
}