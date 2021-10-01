using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Implementations
{
    public class UserRepositoryDF : IUserDAL
    {
        private IConfiguration Configuration { get; set; }

        public UserRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public User GetUserByUserNameAndPassword(string username, string password)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                User result = db.Users.First(u => u.UserName == username && u.Password == password);
                return result;
            }
        }

        public User GetUserById (int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                User result = db.Users.First(u => u.UserId == id);
                return result;
            }
        }
    }
}
