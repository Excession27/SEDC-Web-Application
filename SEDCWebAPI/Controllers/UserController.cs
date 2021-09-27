using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDCWebAPI.Services.Interfaces;
using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace SEDCWebAPI.Controllers
{
    [EnableCors("PolicyOne")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserController(IUserService userService, IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;


        }
        [HttpPost]

        public IActionResult Authenticate([FromBody] AuthenticateDTO authenticateModel)
        {
            UserDTO user = _userService.Authenticate(authenticateModel.UserName, authenticateModel.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            } else
            {
                return Ok(user);
            }
        }
    }
}
