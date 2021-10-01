using AutoMapper;
using SEDCWebApplication.BLL.Logic.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Implementations
{
    public class OrderManager : IOrderManager
    {

        private readonly IOrderDAL _orderDAL;
        private readonly IProductDAL _productDAL;
        private readonly IMapper _mapper;

        public OrderManager(IOrderDAL orderDAL, IProductDAL productDAL, IMapper mapper)
        {
            _orderDAL = orderDAL;
            _productDAL = productDAL;
            _mapper = mapper;
        }

        public string CreateOrderNumber()
        {
            DateTime epoch = DateTime.FromOADate(0);
            DateTime now = DateTime.UtcNow;

            TimeSpan ts = now.Subtract(epoch);
            double unixTimeMilliseconds = ts.TotalMilliseconds;

            string OrderNumber = ("O_" + unixTimeMilliseconds);
            OrderNumber = OrderNumber.Remove(OrderNumber.Length - 5);
            return OrderNumber;
        }

        public OrderDTO Add(OrderDTO orderDto)
        {
            Order order = new Order();
            order.TotalAmount = 0;
            order.OrderNumber = CreateOrderNumber();
            order.OrderDate = DateTime.Now;
            order.OrderItems = new List<OrderItem>();
            order.EmployeeId = orderDto.EmployeeId;
            order.CustomerId = orderDto.CustomerId;

            bool HasOrderInfo = false;

            foreach (OrderItemDTO orderItemDto in orderDto.OrderItems)
            {
                if (orderItemDto.ProductId > 0)
                {
                    Product product = _productDAL.GetById(orderItemDto.ProductId);
                    order.TotalAmount += product.UnitPrice * orderItemDto.Quantity;

                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = order.OrderId;
                    orderItem.ProductId = product.ProductId;
                    orderItem.Quantity = orderItemDto.Quantity;
                    HasOrderInfo = true;
                    order.OrderItems.Add(orderItem);
                }

            }

            if (HasOrderInfo)
            {
                _orderDAL.Save(order);

            }

            return orderDto;
        }

        IEnumerable<OrderDTO> IOrderManager.GetAllOrders()
        {
            throw new NotImplementedException();
        }

        OrderDTO IOrderManager.GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        string IOrderManager.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
