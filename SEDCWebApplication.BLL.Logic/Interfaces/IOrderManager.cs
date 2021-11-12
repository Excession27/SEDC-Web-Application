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
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<IEnumerable<OrderDTO>> GetPreviousOrders(int id);
        Task<OrderDTO> GetOrderById(int id);
        Task<OrderDTO> Add(OrderDTO order);
        Task<OrderDTO> Delete(int id);


    }
}
