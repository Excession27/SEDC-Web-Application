using Microsoft.AspNetCore.Mvc;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.Tests.WebAPI.XUnitTest.Mock
{
    public class DataServiceMock : IDataService
    {
        private List<EmployeeDTO> _employeeList;

        public DataServiceMock()
        {
            _employeeList = new List<EmployeeDTO>
            {
                new EmployeeDTO
                {
                    //EmployeeId=1,
                    EmployeeName="Pera",
                    RoleId=RoleEnum.Manager,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                },
                new EmployeeDTO
                {
                    //EmployeeId=2,
                    EmployeeName="Mika",
                    RoleId=RoleEnum.Sales,
                    ImagePath = "~/img/avatar.png",
                    Test = false
                },
                new EmployeeDTO
                {
                    //EmployeeId=3,
                    EmployeeName="Laza",
                    RoleId=RoleEnum.Operater,
                    ImagePath = "~/img/avatar.png"
                }
            };
        }

        public IActionResult GetAllEmployees()
        {

            return Ok(_employeeList);
        }

    }
}
