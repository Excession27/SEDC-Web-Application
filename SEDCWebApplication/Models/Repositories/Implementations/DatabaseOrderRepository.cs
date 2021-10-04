using SEDCWebApplication.BLL.Logic.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Implementations;
using SEDCWebApplication.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.Models.Repositories.Implementations
{
    public class DatabaseOrderRepository : IOrderRepository
    {
        private readonly IOrderManager _orderManager;
        public DatabaseOrderRepository(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return _orderManager.GetAllOrders();
        }

        public OrderDTO GetOrderById(int id)
        {
            return _orderManager.GetOrderById(id);
        }

        public IEnumerable<OrderDTO> GetPreviousOrders(int customerId)
        {
            return _orderManager.GetPreviousOrders(customerId);
        }

        public OrderDTO Add(OrderDTO order)
        {
            return _orderManager.Add(order);
        }

/*        public OrderDTO Update(OrderDTO order)
        {
            return _orderManager.Update(order);
        }*/

        public string Delete(int id)
        {
            return _orderManager.Delete(id);
        }
    }
}
