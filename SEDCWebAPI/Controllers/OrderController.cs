using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Entities;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SEDCWebAPI.Helpers;
using SEDCWebApplication.DAL.DatabaseFactory.Entities;
using SEDCWebAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEDCWebAPI.Controllers
{

    [EnableCors("PolicyOne")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class OrderController : ControllerBase
    {

            
        private readonly IDataService _dataService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OrderController(IDataService dataService, IWebHostEnvironment hostingEnvironment)
        {
            _dataService = dataService;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            return await _dataService.GetAllOrders();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<OrderDTO> Get(int id)
        {
            return await _dataService.GetOrderById(id);
        }

        
        [HttpGet("orders")]
        public async Task<IActionResult> GetPreviousOrders()
        {
            UserDTO user = (UserDTO)HttpContext.Items["User"];
            IEnumerable<Order> previousOrders = await _dataService.GetPreviousOrders(user.UserId);
            return Ok(previousOrders);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDTO order)
        {
            OrderDTO orderMade = await _dataService.AddOrder(order);
            if (orderMade == null)
            {
                return BadRequest(new { message = "Something is not right with the order. Please try again or if the problem persists contact our support." });
            }
            else
            {
                return Ok(orderMade);
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            return _dataService.DeleteOrder(id);
        }
    }
}
