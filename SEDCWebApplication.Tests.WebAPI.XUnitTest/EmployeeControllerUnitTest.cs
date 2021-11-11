using Microsoft.AspNetCore.Mvc;
using SEDCWebAPI.Controllers;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.Tests.WebAPI.XUnitTest.Mock;
using System;
using System.Collections.Generic;
using Xunit;

namespace SEDCWebApplication.Tests.WebAPI.XUnitTest
{
    public class EmployeeControllerUnitTest
    {
        EmployeeController _controller;
        IDataService _service;

        public EmployeeControllerUnitTest()
        {
            _service = new DataServiceMock();
            _controller = new EmployeeController(_service, null);
            
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            var items = Assert.IsType<List<EmployeeDTO>>(okResult.Value);
            Assert.Equal(4, items.Count);
        }
    }
}
