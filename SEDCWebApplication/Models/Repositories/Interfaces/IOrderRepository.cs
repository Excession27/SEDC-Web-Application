using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.Models.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetPreviousOrders(int customerId);
        OrderDTO GetOrderById(int id);
        OrderDTO Add(OrderDTO order);
        string Delete(int id);
    }
}
