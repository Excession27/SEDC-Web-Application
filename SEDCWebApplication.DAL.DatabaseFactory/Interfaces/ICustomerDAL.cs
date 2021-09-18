using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.DatabaseFactory.Interfaces
{
    public interface ICustomerDAL
    {
        void Save(Customer item);

        Customer GetById(int id);

        List<Customer> GetAll(int skip, int take);

        void Update(Customer item);

    }
}
