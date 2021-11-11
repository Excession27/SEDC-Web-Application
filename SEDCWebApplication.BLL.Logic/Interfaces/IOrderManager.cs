using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface IOrderManager
    {
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<Order> GetPreviousOrders(int id);
        OrderDTO GetOrderById(int id);
        Task<OrderDTO> Add(OrderDTO order);
        string Delete(int id);


    }
}
