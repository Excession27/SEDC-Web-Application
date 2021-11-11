using SEDCWebAPI.Controllers;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.XUnitTest.Mock
{
    public class EmployeeRepositoryMock : IEmployeeDAL
    {
        private List<Employee> _employeeList;

        public EmployeeRepositoryMock()
        {
            _employeeList = new List<Employee>
            {
                new Employee
                {
                    UserId=1,
                    EmployeeName="Pera",
                    RoleId=1,
                    ImagePath = "~/img/avatar.png",
                    DateOfBirth = new DateTime(1990, 5, 19)
                    
                },
                new Employee
                {
                    UserId=2,
                    EmployeeName="Mika",
                    RoleId=2,
                    ImagePath = "~/img/avatar.png",
                    DateOfBirth = new DateTime(1980, 2, 5)

                },
                new Employee
                {
                    UserId=3,
                    EmployeeName="Laza",
                    RoleId=3,
                    ImagePath = "~/img/avatar.png",
                    DateOfBirth = new DateTime(1998, 10, 1)
                },
                new Employee
                {
                    UserId=4,
                    EmployeeName="Milica",
                    RoleId=2,
                    ImagePath = "~/img/avatar.png",
                    DateOfBirth = new DateTime(1992, 1, 22)
                }
            };
        }

        public List<Employee> GetAll(int skip, int take)
        {
            return _employeeList;
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Employee item)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
