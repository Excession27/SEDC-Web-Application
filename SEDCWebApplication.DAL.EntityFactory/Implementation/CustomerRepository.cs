using SEDCWebApplication.DAL.EntityFactory.Entities;
using SEDCWebApplication.DAL.EntityFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDCWebApplication.DAL.EntityFactory.Implementation
{
    public class CustomerRepository : ICustomerDAL
    {
        public List<Customer> GetAll(int skip, int take)
        {
            using var db = new PizzaShop3Context();
            List<Customer> result = db.Customers.Skip(skip).Take(take).ToList();
            return result;
            
        }

        public Customer GetById(int id)
        {
            using var db = new PizzaShop3Context();
            Customer result = db.Customers.First(emp => emp.CustomerId == id);
            return result;
        }

        public void Save(Customer item)
        {
            using var db = new PizzaShop3Context();
            User user = new User();
            user.Customer = item;
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Update(Customer item)
        {
            throw new NotImplementedException();
        }
    }
}
