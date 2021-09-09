using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.Data.Entities
{
    public class Customer : User
    {
        public Customer(int? id)
        : base(id)
        {
        }
        public string Name { get; set; }

        public int ContactId { get; set; }

       

    }
}
