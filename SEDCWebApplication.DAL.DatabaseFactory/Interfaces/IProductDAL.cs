using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.DAL.DatabaseFactory.Interfaces
{
    public interface IProductDAL
    {
        Task Save(Product item);

        Task<Product> GetById(int id);

        Task<List<Product>> GetAll(int skip, int take);

        Task<Product> Update(Product item);

        Task<Product> Delete(int id);
    }
}
