using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System.Linq;

namespace SEDCWebApplication.DAL.DatabaseFactory.Implementations
{
    public class CustomerRepositoryDF : ICustomerDAL
    {
        private IConfiguration Configuration { get; set; }

        public CustomerRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Save(Customer item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                User user = new User();
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public Customer GetById(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Customer result = db.Customers.First(e => e.UserId == id);
                return result;
            }
        }

        public List<Customer> GetAll(int skip, int take)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Customer> result = db.Customers.Skip(skip).Take(take).ToList();
                return result;
            }
        }

        public void Update(Customer item)
        {

        }

    }
}
