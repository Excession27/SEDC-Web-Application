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
    public class EmployeeRepositoryDF : IEmployeeDAL
    {
        private IConfiguration Configuration { get; set; }

        public EmployeeRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Save(Employee item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                if (item.Role == null)
                {
                    item.Role = new Role();
                }
                
                db.Users.Add(item);
                db.SaveChanges();
            }
        }

        public Employee GetById(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Employee result = db.Employees.First(e => e.UserId == id);
                return result;
            }
        }

        public List<Employee> GetAll(int skip, int take)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Employee> result = db.Employees.Skip(skip).Take(take).ToList();
                return result;
            }
        }

        public void Update(Employee item)
        {

        }

    }
}
