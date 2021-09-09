using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.Data.Entities
{
    public class User : BaseEntity
    {
        public User(int? id)
    : base(id)
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
