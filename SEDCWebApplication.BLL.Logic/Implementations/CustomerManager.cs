using AutoMapper;
using SEDCWebApplication.BLL.Logic.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;

// ADO.net
//using SEDCWebApplication.DAL.Data.Entities;
//using SEDCWebApplication.DAL.Data.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

//EntityFramework - Entity Factory
//using SEDCWebApplication.DAL.EntityFactory.Interfaces;
//using SEDCWebApplication.DAL.EntityFactory.Entities;

//EntityFramework - Database Factory
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System.Threading.Tasks;
using SEDCWebApplication.DAL.DatabaseFactory.GenericRepository;
using Microsoft.Extensions.Configuration;

namespace SEDCWebApplication.BLL.Logic.Implementations
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IGenericRepository<Customer> _customerDAL;
        private readonly IMapper _mapper;
        private readonly IOrderDAL _orderDAL;
        private readonly IConfiguration _configuration;
        public CustomerManager(IGenericRepository<Customer> customerDAL, IOrderDAL orderDAL, IMapper mapper, IConfiguration configuration)
        {
            _customerDAL = customerDAL;
            _orderDAL = orderDAL;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<CustomerDTO> Add(CustomerDTO customer)
        {
            
            Customer customerEntity = _mapper.Map<Customer>(customer);
            await _customerDAL.Save(customerEntity);
            customer = _mapper.Map<CustomerDTO>(customerEntity);
            return customer;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            return _mapper.Map<List<CustomerDTO>>(await _customerDAL.GetAll(0, 50));
        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            return _mapper.Map<CustomerDTO>(await _customerDAL.GetById(id));
        }
    }
}
