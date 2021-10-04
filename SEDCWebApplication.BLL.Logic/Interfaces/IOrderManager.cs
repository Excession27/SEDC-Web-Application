using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface IOrderManager
    {
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetPreviousOrders(int customerId);
        OrderDTO GetOrderById(int id);
        OrderDTO Add(OrderDTO order);
        string Delete(int id);


    }
}
