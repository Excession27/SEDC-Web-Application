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
    public class ProductRepositoryDF : IProductDAL
    {
        private IConfiguration Configuration { get; set; }

        public ProductRepositoryDF(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task Save(Product item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Product product = new Product();
                product = item;
                await db.Products.AddAsync(product);
                db.SaveChanges();
            }
        }

        public async Task<Product> GetById(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Product result = await db.Products.FirstAsync(e => e.ProductId == id);
                return result;
            }
        }

        public async Task<List<Product>> GetAll(int skip, int take)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                List<Product> result = await db.Products.Skip(skip).Take(take).ToListAsync();
                return result;
            }
        }

        public async Task<Product> Update(Product item)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Product result = db.Products.Update(item).Entity;
                return result;
            }
        }

        public async Task<Product> Delete(int id)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.GetConnectionString("SEDCEF"));
            using (var db = new ApplicationDbContext(optionBuilder.Options))
            {
                Product result = await db.Products.Where(product => product.ProductId == id).FirstAsync();
                result.IsDeleted = true;
                db.SaveChanges();
                return result;
            }    
        }
    }
}
