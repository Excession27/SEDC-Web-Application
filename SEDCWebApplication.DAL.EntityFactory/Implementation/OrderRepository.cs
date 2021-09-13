using Microsoft.EntityFrameworkCore;
using SEDCWebApplication.DAL.EntityFactory.Entities;
using SEDCWebApplication.DAL.EntityFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDCWebApplication.DAL.EntityFactory.Implementation
{
    public class OrderRepository : IOrderDAL
    {
        public List<Order> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetByEmployeeId(int id)
        {
            using (var db = new PizzaShop3Context())
            {
                List<Order> result = db.Orders
                                            .Include(o => o.Customer)
                                            .Where(e => e.EmployeeId == id)
                                            .ToList();
                return result;
            }
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
