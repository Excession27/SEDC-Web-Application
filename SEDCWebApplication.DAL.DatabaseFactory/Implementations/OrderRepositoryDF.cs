using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.DAL.DatabaseFactory.Implementations
{
    public class OrderRepositoryDF : IOrderDAL
    {
        private IConfiguration Configuration { get; set; }

        public OrderRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async void Save(Order item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Order order = new Order();
                order = item;
                await db.Orders.AddAsync(order);
                db.SaveChanges();
            }
        }

        public async Task<Order> GetById(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Order result = await db.Orders.FirstAsync(e => e.OrderId == id);
                
                return result;
            }
        }

        public async Task<List<Order>> GetAll(int skip, int take)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDC2"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Order> result = await db.Orders.Skip(skip).Take(take).ToListAsync();
                return result;
            }
        }

        public async Task<List<Order>> GetPreviousOrders(int skip, int take, int customerId)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Order> result = await db.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).Where(item => item.CustomerId == customerId).Skip(skip).Take(take).ToListAsync();
                
                return result;
            }
        }

        public void Update(Order item)
        {

        }

        public async Task<List<Order>> GetByEmployeeId(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDC2"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Order> result = await db.Orders
                                            .Include(o => o.Customer)
                                            .Where(e => e.EmployeeId == id)
                                            .ToListAsync();
                
                return result;
            }
        }
    }
}
