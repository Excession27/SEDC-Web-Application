using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using SEDCWebApplication.DAL.DatabaseFactory.GenericRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebAPI.Services.Implementation
{
    public class DataService : IDataService
    {
        private readonly IEmployeeManager _employeeManager;

        private readonly ICustomerManager _customerManager;

        private readonly IOrderManager _orderManager;

        private readonly IProductManager _productManager;

        private readonly IGenericRepository<Employee> _genericEmployeeManager;

        private readonly IConfiguration _configuration;

        public DataService(IEmployeeManager employeeManager, ICustomerManager customerManager, IOrderManager orderManager, IProductManager productManager, IConfiguration configuration)
        {
            _employeeManager = employeeManager;
            _customerManager = customerManager;
            _orderManager = orderManager;
            _productManager = productManager;
            _configuration = configuration;
        }

        //                  Employee
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            return await _employeeManager.GetEmployeeById(id);

        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            return await _employeeManager.GetAllEmployees();
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employee)
        {
            return await _employeeManager.Add(employee);
        }
        public async Task<CustomerDTO> UpdateEmployee()
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }




        //                  Customer

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            return await _customerManager.GetCustomerById(id);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            return await _customerManager.GetAllCustomers();
        }

        public async Task<CustomerDTO> AddCustomer(CustomerDTO customer)
        {
            return await _customerManager.Add(customer);
        }


        public void DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }



        //                  Order


        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            return await _orderManager.Add(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetPreviousOrders(int id)
        {
            return await _orderManager.GetPreviousOrders(id);
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return await _orderManager.GetOrderById(id);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            return await _orderManager.GetAllOrders();
        }

        public async Task<OrderDTO> DeleteOrder(int id)
        {
            return await _orderManager.Delete(id);
        }

        //                  Product

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _productManager.GetAllProducts();
        }

        public async Task<ProductDTO> AddProduct(ProductDTO product)
        {
            return await _productManager.Add(product);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            return await _productManager.GetProductById(id);
        }

        public async Task<ProductDTO> DeleteProduct(int id)
        {
            return await _productManager.Delete(id);
        }
    }
}
