using SEDCWebApplication.DAL.EntityFactory.Entities;
using SEDCWebApplication.DAL.EntityFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDCWebApplication.DAL.EntityFactory.Implementation
{
    public class EmployeeRepository : IEmployeeDAL
    {
        public List<Employee> GetAll(int skip, int take)
        {
            using var db = new PizzaShop3Context();
            List<Employee> result = db.Employees.Skip(skip).Take(take).ToList();
            return result;
            
        }

        public Employee GetById(int id)
        {
            using var db = new PizzaShop3Context();
            Employee result = db.Employees.First(emp => emp.EmployeeId == id);
            return result;
        }

        public void Save(Employee item)
        {
            using var db = new PizzaShop3Context();
            User user = new User();
            user.Employee = item;
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
