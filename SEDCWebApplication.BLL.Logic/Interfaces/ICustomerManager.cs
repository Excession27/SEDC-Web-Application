using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface ICustomerManager
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> GetCustomerById(int id);
        Task<CustomerDTO> Add(CustomerDTO customer);
    }
}
