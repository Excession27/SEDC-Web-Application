using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Models
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }

        
        public string OrderNumber { get; set; }
        
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int OrderStatusId { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

    }
}
