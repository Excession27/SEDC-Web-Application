using AutoMapper;
using SEDCWebApplication.BLL.Logic.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;

// ADO.net
//using SEDCWebApplication.DAL.Data.Entities;
//using SEDCWebApplication.DAL.Data.Interfaces;

// EntityFramework - Entity Factory
//using SEDCWebApplication.DAL.EntityFactory.Interfaces;
//using SEDCWebApplication.DAL.EntityFactory.Entities;

// EntityFramework - Database Factory
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using Microsoft.Extensions.Configuration;
using SEDCWebApplication.DAL.DatabaseFactory.GenericRepository;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Implementations
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IGenericRepository<Employee> _employeeDAL;
        private readonly IMapper _mapper;
        private readonly IOrderDAL _orderDAL;
        private readonly IConfiguration _configuration;
        public EmployeeManager(IGenericRepository<Employee> employeeDAL, IOrderDAL orderDAL, IMapper mapper, IConfiguration configuration)
        {
            _employeeDAL = employeeDAL;
            _orderDAL = orderDAL;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<EmployeeDTO> Add(EmployeeDTO employee)
        {
            Employee employeeEntity = _mapper.Map<Employee>(employee);
            await _employeeDAL.Save(employeeEntity);
            employee = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employee;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            return _mapper.Map<List<EmployeeDTO>>(await _employeeDAL.GetAll(0, 50));
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            try
            {
                Employee employee = await _employeeDAL.GetById(id);
                if (employee == null)
                {
                    throw new Exception($"Employee with id {id} not found.");
                }
                EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                employeeDTO.Orders = await _orderDAL.GetByEmployeeId(employee.UserId);
                return employeeDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EmployeeDTO> Update(EmployeeDTO employee)
        {
            Employee employeeEntity = _mapper.Map<Employee>(employee);
            await _employeeDAL.Update(employeeEntity);
            employee = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employee;
        }
    }
}
