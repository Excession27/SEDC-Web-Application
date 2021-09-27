using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ImagePath { get; set; }

        public int RoleId { get; set; }

    }
}
