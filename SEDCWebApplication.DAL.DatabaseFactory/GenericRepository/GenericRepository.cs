using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.DAL.DatabaseFactory.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> table;
        public GenericRepository(IConfiguration configuration)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(configuration.GetConnectionString("SEDCEF"));
            _context = new ApplicationDbContext(optionBuilder.Options);
            table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(int skip, int take)
        {
            List<T> result = await table.Skip(skip).Take(take).ToListAsync();
            return result;
        }

        public async Task<T> GetById(int id)
        {
            T result = await table.FindAsync(id);
            return result;
        }

        public async Task Save(T item)
        {
            await table.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Update(T item)
        {
            table.Update(item);
            _context.SaveChanges();
        }


    }
}
