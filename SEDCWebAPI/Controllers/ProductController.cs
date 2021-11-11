using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEDCWebAPI.Helpers;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Entities;

using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEDCWebAPI.Controllers
{

    [EnableCors("PolicyOne")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationRoles.Admin)]

    public class ProductController : ControllerBase
    {

            
        private readonly IDataService _dataService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IDataService dataService, IWebHostEnvironment hostingEnvironment)
        {
            _dataService = dataService;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: api/<ProductController>
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            
            IEnumerable<ProductDTO> allProducts = await _dataService.GetAllProducts();
            return Ok(allProducts);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ProductDTO> Get(int id)
        {
            return await _dataService.GetProductById(id);
        }


        // POST api/<ProductController>
        [HttpPost]
        public async Task<ProductDTO> Post([FromBody] ProductDTO product)
        {
            return await _dataService.AddProduct(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ProductDTO> Delete(int id)
        {
            return await _dataService.DeleteProduct(id);
        }
    }
}
