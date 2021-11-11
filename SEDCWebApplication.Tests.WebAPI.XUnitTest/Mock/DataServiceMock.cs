using Microsoft.AspNetCore.Mvc;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    EmployeeId=1,
                    EmployeeName="Dragan",
                    RoleId=RoleEnum.Manager,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                },
                new EmployeeDTO
                {
                    EmployeeId=2,
                    EmployeeName="Sara",
                    RoleId=RoleEnum.Manager,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                },
                new EmployeeDTO
                {
                    EmployeeId=3,
                    EmployeeName="Joca",
                    RoleId=RoleEnum.Operater,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                },
                new EmployeeDTO
                {
                    EmployeeId=4,
                    EmployeeName="Vesna",
                    RoleId=RoleEnum.Sales,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                }
            };
        }
        public EmployeeDTO AddEmployee(EmployeeDTO employee)
        {
            employee.EmployeeId = _employeeList.Max(e => e.EmployeeId) + 1;
            _employeeList.Add(employee);
            return _employeeList.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
        }


        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            return _employeeList;
        }



    }
}
