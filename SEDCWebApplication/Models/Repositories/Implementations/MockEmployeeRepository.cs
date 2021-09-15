using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.Models.Repositories.Implementations
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<EmployeeDTO> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<EmployeeDTO>
            {
                new EmployeeDTO
                {
                    EmployeeId=1,
                    EmployeeName="Pera",
                    Role=RoleEnum.Manager,
                    ImagePath = "~/img/avatar.png",
                    Test = true
                },
                new EmployeeDTO
                {
                    EmployeeId=2,
                    EmployeeName="Mika",
                    Role=RoleEnum.Sales,
                    ImagePath = "~/img/avatar.png",
                    Test = false
                },
                new EmployeeDTO
                {
                    EmployeeId=3,
                    EmployeeName="Laza",
                    Role=RoleEnum.Operater,
                    ImagePath = "~/img/avatar.png"
                }
            };
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            return _employeeList;
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            return _employeeList.Where(x => x.EmployeeId == id).FirstOrDefault();
        }

        public EmployeeDTO Add(EmployeeDTO employee)
        {
            employee.EmployeeId = _employeeList.Max(e => e.EmployeeId) + 1;
            _employeeList.Add(employee);
            return _employeeList.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
        }

        public EmployeeDTO Update(EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}
