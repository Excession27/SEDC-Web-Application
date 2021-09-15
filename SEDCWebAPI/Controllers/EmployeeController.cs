using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEDCWebApplication.BLL.Logic.Models;

using SEDCWebApplication.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEDCWebAPI.Controllers
{


    [Route("api/[controller]")]
    [EnableCors("PolicyOne")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

            
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;


        }

        // GET: api/<EmployeeController>
        
        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {
            return _employeeRepository.GetAllEmployees().ToList(); ;
        }

        // GET api/<EmployeeController>/5
        
        [HttpGet("{id}")]
        public EmployeeDTO Get(int id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] EmployeeDTO employee)
        {
            _employeeRepository.Add(employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
