using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; }


        public string Token { get; set; }

        public string Role { get; set; }
    }
}
