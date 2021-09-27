using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Models
{
    public class OrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }
    }
}
