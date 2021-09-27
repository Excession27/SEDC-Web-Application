using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal? TotalAmount { get; set; }
        public int Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("EmployeeId")]
        public  Employee Employee { get; set; }
    }
}
