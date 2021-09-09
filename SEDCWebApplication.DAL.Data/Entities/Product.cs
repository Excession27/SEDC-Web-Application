using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.Data.Entities
{
    public class Product : User
    {
        public Product(int? id)
        : base(id)
        {
        }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool? IsDiscounted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Size { get; set; }
        public string ImagePath { get; set; }
    }
}
