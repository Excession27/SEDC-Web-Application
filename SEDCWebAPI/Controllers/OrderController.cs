using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.DAL.Data.Entities;
using SEDCWebApplication.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Cors;
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
    public class OrderController : ControllerBase
    {

            
        private readonly IOrderRepository _orderRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OrderController(IOrderRepository orderRepository, IWebHostEnvironment hostingEnvironment)
        {
            _orderRepository = orderRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<OrderDTO> GetAll()
        {
            return _orderRepository.GetAllOrders().ToList(); ;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public OrderDTO Get(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO order)
        {
            OrderDTO orderMade = _orderRepository.Add(order);
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
        public string Delete(int id)
        {
            return _orderRepository.Delete(id);
        }
    }
}
