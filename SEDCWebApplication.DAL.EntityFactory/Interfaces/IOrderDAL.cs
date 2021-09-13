using SEDCWebApplication.DAL.EntityFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.DAL.EntityFactory.Interfaces
{
    public interface IOrderDAL
    {
        void Save(Order item);

        Order GetById(int id);

        List<Order> GetByEmployeeId(int id);

        List<Order> GetAll(int skip, int take);
    }
}
