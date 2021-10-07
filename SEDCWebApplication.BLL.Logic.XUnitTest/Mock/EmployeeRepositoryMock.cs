using SEDCWebApplication.DAL.EntityFactory.Entities;
using SEDCWebApplication.DAL.EntityFactory.Interfaces;
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
                    //EmployeeId=1,
                    EmployeeName="Pera",
                    RoleId=1,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                },
                new Employee
                {
                    //EmployeeId=2,
                    EmployeeName="Mika",
                    RoleId=2,
                    ImagePath = "~/img/avatar.png",
                    Test = false
                },
                new Employee
                {
                    //EmployeeId=3,
                    EmployeeName="Laza",
                    RoleId=3,
                    ImagePath = "~/img/avatar.png"
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
