using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class Order
    {
        public Order()
        {
            //OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? OrderStatusId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        //public Customer Customer { get; set; }
        public  Employee Employee { get; set; }
        //public ICollection<OrderItem> OrderItems { get; set; }
    }
}
