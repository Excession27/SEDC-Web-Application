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
        public ProductDTO Add(ProductDTO product)
        {
            Product productEntity = _mapper.Map<Product>(product);
            _productDAL.Save(productEntity);
            product = _mapper.Map<ProductDTO>(productEntity);
            return product;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _mapper.Map<List<ProductDTO>>(_productDAL.GetAll(0, 50));
        }

        public ProductDTO GetProductById(int id)
        {
            return _mapper.Map<ProductDTO>(_productDAL.GetById(id));
        }
        public ProductDTO Update(ProductDTO product)
        {
            Product productEntity = _mapper.Map<Product>(product);
            _productDAL.Update(productEntity);
            product = _mapper.Map<ProductDTO>(productEntity);
            return product;
        }

        public string Delete(int id)
        {
            return _productDAL.Delete(id);
        }            
    }
}
