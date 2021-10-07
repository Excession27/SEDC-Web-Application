using Microsoft.AspNetCore.Mvc;
using SEDCWebApplication.BLL.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebAPI.Services.Interfaces
{
    public interface IDataService
    {

        IActionResult GetAllEmployees();
    }
}
