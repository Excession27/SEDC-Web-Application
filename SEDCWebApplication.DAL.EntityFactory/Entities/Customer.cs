using System;
using System.Collections.Generic;

#nullable disable

namespace SEDCWebApplication.DAL.EntityFactory.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ContactId { get; set; }
        public string ImagePath { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual User CustomerNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
