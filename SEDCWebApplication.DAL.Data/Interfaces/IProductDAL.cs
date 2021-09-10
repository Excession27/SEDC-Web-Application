using SEDCWebApplication.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.Data.Interfaces
{
    public interface IProductDAL
    {
        void Save(Product item);

        Product GetById(int id);

        List<Product> GetAll(int skip, int take);

        void Update(Product item);
        string Delete(int id);
    }
}
