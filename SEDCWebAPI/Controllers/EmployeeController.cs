using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEDCWebApplication.BLL.Logic.Models;

using Microsoft.AspNetCore.Mvc.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using SEDCWebAPI.Helpers;
using SEDCWebAPI.Services.Interfaces;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEDCWebAPI.Controllers
{


    [Route("api/[controller]")]
    [EnableCors("PolicyOne")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

            
        
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDataService _dataService;

        public EmployeeController(IDataService dataService, IWebHostEnvironment hostingEnvironment)
        {
            
            _hostingEnvironment = hostingEnvironment;
            _dataService = dataService;


        }

        // GET: api/<EmployeeController>
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            var result = await _dataService.GetAllEmployees(); // ToListAsync???
            return Ok(result);
        }

        // GET api/<EmployeeController>/5
        
        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetById(int id)
        {
            return await _dataService.GetEmployeeById(id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<EmployeeDTO> Post([FromBody] EmployeeDTO employee)
        {
            return await _dataService.AddEmployee(employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EmployeeDTO employee)
        {
            // TODO
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dataService.DeleteEmployee(id);
        }
    }
}
