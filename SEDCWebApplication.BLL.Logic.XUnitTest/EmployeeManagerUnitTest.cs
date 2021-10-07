using AutoMapper;
using Microsoft.Extensions.Configuration;
using SEDCWebApplication.BLL.Logic.Implementations;
using SEDCWebApplication.BLL.Logic.XUnitTest.Mock;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using Xunit;

namespace SEDCWebApplication.BLL.Logic.XUnitTest
{
    public class EmployeeManagerUnitTest
    {
        EmployeeManager _manager;
        IEmployeeDAL _employeeDAL;
        IOrderDAL _orderDAL;
        IMapper _mapper;
        IConfiguration _configuration;

        public EmployeeManagerUnitTest()
        {
            //_employeeDAL = new EmployeeRepositoryMock();
           
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
