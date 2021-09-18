using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class OrderItem
    {
        
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
