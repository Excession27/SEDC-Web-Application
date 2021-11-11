using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Interfaces
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> Add(ProductDTO employee);
        Task<ProductDTO> Update(ProductDTO employee);
        Task<ProductDTO> Delete(int id);
    }
}
