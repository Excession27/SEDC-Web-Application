using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface IUserManager
    {


        UserDTO GetUserByUserNameAndPassword(string username, string password);
    }
}
