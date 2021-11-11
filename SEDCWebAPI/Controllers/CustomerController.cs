using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Entities;
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
    public class CustomerController : ControllerBase
    {

            
        private readonly IDataService _dataService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CustomerController(IDataService dataService, IWebHostEnvironment hostingEnvironment)
        {
            _dataService = dataService;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            return await _dataService.GetAllCustomers();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            return await _dataService.GetCustomerById(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] CustomerDTO customer)
        {
            _dataService.AddCustomer(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CustomerDTO customer)
        {
            // TODO
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dataService.DeleteCustomer(id);
        }
    }
}
