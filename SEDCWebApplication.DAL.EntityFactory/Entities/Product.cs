using System;
using System.Collections.Generic;

#nullable disable

namespace SEDCWebApplication.DAL.EntityFactory.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsDiscounted { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string Size { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
