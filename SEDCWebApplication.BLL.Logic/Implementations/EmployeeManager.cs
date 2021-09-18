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


namespace SEDCWebApplication.BLL.Logic.Implementations
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeDAL _employeeDAL;
        private readonly IMapper _mapper;
        private readonly IOrderDAL _orderDAL;
        public EmployeeManager(IEmployeeDAL employeeDAL, IMapper mapper, IOrderDAL orderDAL)
        {
            _employeeDAL = employeeDAL;
            _orderDAL = orderDAL;
            _mapper = mapper;
        }
        public EmployeeDTO Add(EmployeeDTO employee)
        {
            Employee employeeEntity = _mapper.Map<Employee>(employee);
            _employeeDAL.Save(employeeEntity);
            employee = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employee;
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            return _mapper.Map<List<EmployeeDTO>>(_employeeDAL.GetAll(0, 50));
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            try
            {
                Employee employee = _employeeDAL.GetById(id);
                if (employee == null)
                {
                    throw new Exception($"Employee with id {id} not found.");
                }
                EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                employeeDTO.Orders = _orderDAL.GetByEmployeeId(employee.UserId);
                return employeeDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EmployeeDTO Update(EmployeeDTO employee)
        {
            Employee employeeEntity = _mapper.Map<Employee>(employee);
            _employeeDAL.Update(employeeEntity);
            employee = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employee;
        }
    }
}
