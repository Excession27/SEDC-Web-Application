using AutoMapper;
using Microsoft.Extensions.Configuration;
using SEDCWebApplication.BLL.Logic.Implementations;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.BLL.Logic.XUnitTest.Mock;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using System.Collections.Generic;
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
            var myConfiguration = new Dictionary<string, string> {
                {"AppSettings:ImageRoot",  "http:\\localhost:44380/img/"},
                {"AppSettings:Secret", "SEDCWebAPISecretKey"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile(_configuration));
            }).CreateMapper();

            _employeeDAL = new EmployeeRepositoryMock();
            _manager = new EmployeeManager(_employeeDAL, _orderDAL, _mapper, _configuration);
        }

        [Fact]
        public void GetAllEmployees_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _manager.GetAllEmployees();

            // Assert
            var items = Assert.IsType<List<EmployeeDTO>>(result);
            Assert.Equal(4, items.Count);
        }
    }
}
