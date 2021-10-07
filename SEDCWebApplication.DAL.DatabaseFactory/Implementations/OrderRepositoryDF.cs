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
    public class OrderRepositoryDF : IOrderDAL
    {
        private IConfiguration Configuration { get; set; }

        public OrderRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Save(Order item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Order order = new Order();
                order = item;
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public Order GetById(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Order result = db.Orders.First(e => e.OrderId == id);
                return result;
            }
        }

        public List<Order> GetAll(int skip, int take)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDC2"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Order> result = db.Orders.Skip(skip).Take(take).ToList();
                return result;
            }
        }

        public List<Order> GetPreviousOrders(int skip, int take, int customerId)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Order> result = db.Orders.Include("OrderItems").Where(item => item.CustomerId == customerId).Skip(skip).Take(take).ToList();
                
                return result;
            }
        }

        public void Update(Order item)
        {

        }

        public List<Order> GetByEmployeeId(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDC2"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Order> result = db.Orders
                                            .Include(o => o.Customer)
                                            .Where(e => e.EmployeeId == id)
                                            .ToList();
                return result;
            }
        }
    }
}
