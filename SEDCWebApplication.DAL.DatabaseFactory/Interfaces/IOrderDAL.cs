using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.DAL.DatabaseFactory.Interfaces
{
    public interface IOrderDAL
    {
        void Save(Order item);

        Task<Order> GetById(int id);

        Task<List<Order>> GetByEmployeeId(int id);

        Task<List<Order>> GetAll(int skip, int take);

        Task<List<Order>> GetPreviousOrders(int skip, int take, int id);
    }
}
