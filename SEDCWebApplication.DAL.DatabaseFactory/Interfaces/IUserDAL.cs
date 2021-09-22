using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Interfaces
{
    public interface IUserDAL
    {
        User GetUserByUserNameAndPassword(string username, string password);
    }
}
