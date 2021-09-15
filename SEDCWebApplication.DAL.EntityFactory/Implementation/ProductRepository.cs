using SEDCWebApplication.DAL.EntityFactory.Entities;
using SEDCWebApplication.DAL.EntityFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDCWebApplication.DAL.EntityFactory.Implementation
{
    public class ProductRepository : IProductDAL
    {
        public List<Product> GetAll(int skip, int take)
        {
            using var db = new PizzaShop3Context();
            List<Product> result = db.Products.Skip(skip).Take(take).ToList();
            return result;
            
        }

        public Product GetById(int id)
        {
            using var db = new PizzaShop3Context();
            Product result = db.Products.First(emp => emp.ProductId == id);
            return result;
        }

        public void Save(Product item)
        {
            using var db = new PizzaShop3Context();

            db.Products.Add(item);
            db.SaveChanges();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public string Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
