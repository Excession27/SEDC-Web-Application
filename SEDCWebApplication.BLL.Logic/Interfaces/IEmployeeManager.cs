using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface IEmployeeManager
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeDTO> Add(EmployeeDTO employee);
        Task<EmployeeDTO> Update(EmployeeDTO employee);
    }
}
