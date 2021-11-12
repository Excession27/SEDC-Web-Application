using Microsoft.AspNetCore.Mvc;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebAPI.Services.Interfaces
{
    public interface IDataService
    {
        //                  Employee
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeDTO> AddEmployee(EmployeeDTO employee);

        void DeleteEmployee(int id);

        //                  Customer

        Task<CustomerDTO> GetCustomerById(int id);
        Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> AddCustomer(CustomerDTO customer);

        Task<CustomerDTO> UpdateEmployee();

        void DeleteCustomer(int id);

        //                  Order

        Task<OrderDTO> AddOrder(OrderDTO order);

        Task<IEnumerable<OrderDTO>> GetPreviousOrders(int id);

        Task<OrderDTO> GetOrderById(int id);

        Task<IEnumerable<OrderDTO>> GetAllOrders();

        Task<OrderDTO> DeleteOrder(int id);

        //                  Product

        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> AddProduct(ProductDTO product);
        Task<ProductDTO> DeleteProduct(int id);

    }
}
