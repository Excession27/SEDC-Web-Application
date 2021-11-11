using AutoMapper;
using SEDCWebApplication.BLL.Logic.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
// ADO.net
//using SEDCWebApplication.DAL.Data.Entities;
//using SEDCWebApplication.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
// EntityFramework - Entity Factory
//using SEDCWebApplication.DAL.EntityFactory.Interfaces;
//using SEDCWebApplication.DAL.EntityFactory.Entities;

// EntityFramework - Database Factory
using SEDCWebApplication.DAL.DatabaseFactory.Interfaces;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Implementations
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDAL _productDAL;
        private readonly IMapper _mapper;
        public ProductManager(IProductDAL productDAL, IMapper mapper)
        {
            _productDAL = productDAL;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Add(ProductDTO product)
        {
            Product productEntity = _mapper.Map<Product>(product);
            await _productDAL.Save(productEntity);
            product = _mapper.Map<ProductDTO>(productEntity);
            return product;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return _mapper.Map<List<ProductDTO>>(await _productDAL.GetAll(0, 50));
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            return _mapper.Map<ProductDTO>( await _productDAL.GetById(id));
        }
        public async Task<ProductDTO> Update(ProductDTO product)
        {
            Product productEntity = _mapper.Map<Product>(product);
            await _productDAL.Update(productEntity);
            product = _mapper.Map<ProductDTO>(productEntity);
            return product;
        }

        public async Task<ProductDTO> Delete(int id)
        {
            return _mapper.Map<ProductDTO>(await _productDAL.Delete(id));
        }            
    }
}
