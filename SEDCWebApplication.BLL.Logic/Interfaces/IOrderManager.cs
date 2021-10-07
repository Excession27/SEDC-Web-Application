﻿using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface IOrderManager
    {
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<Order> GetPreviousOrders(int customerId);
        OrderDTO GetOrderById(int id);
        OrderDTO Add(OrderDTO order);
        string Delete(int id);


    }
}
