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
    public class ProductRepositoryDF : IProductDAL
    {
        private IConfiguration Configuration { get; set; }

        public ProductRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Save(Product item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Product product = new Product();
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public Product GetById(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Product result = db.Products.First(e => e.ProductId == id);
                return result;
            }
        }

        public List<Product> GetAll(int skip, int take)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Product> result = db.Products.Skip(skip).Take(take).ToList();
                return result;
            }
        }

        public void Update(Product item)
        {

        }

        public string Delete(int id)
        {
            return "Not implemented yet :)";
        }
    }
}
