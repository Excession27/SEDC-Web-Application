using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEDCWebApplication.BLL.Logic.Models;
using SEDCWebApplication.Models;
using SEDCWebApplication.Models.Repositories.Implementations;
using SEDCWebApplication.Models.Repositories.Interfaces;
using SEDCWebApplication.ViewModels;

namespace SEDCWebApplication.Controllers
{
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;


        }


        [Route("List")]
        public IActionResult List()
        {

            List<EmployeeDTO> employees = _employeeRepository.GetAllEmployees().ToList();
            ViewBag.Title = "Employees";

            return View(employees);
        }

        [Route("DetailsView/{id}")]
        public IActionResult Details(int id)
        {

            
            EmployeeDTO employee = _employeeRepository.GetEmployeeById(id);



            EmployeeDetailsViewModel employeeVM = new EmployeeDetailsViewModel();
            employeeVM.EmployeeName = employee.EmployeeName;
            employeeVM.PageTitle = "Employee's details";
            employeeVM.ImagePath = employee.ImagePath;
            employeeVM.Id = (int)employee.EmployeeId;

            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = "avatar.png";
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }


                EmployeeDTO employee = new EmployeeDTO
                {
                    EmployeeId = null,
                    EmployeeName = model.Name,
                    RoleId = model.Role,
                    ImagePath = "/img/" + uniqueFileName
                };
                EmployeeDTO newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("Details", new { id = newEmployee.EmployeeId });
            } else
            {
                return View();
            }
            
        }

        
        [Route("Edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            EmployeeDTO employee = _employeeRepository.GetEmployeeById(id);



            EmployeeUpdateViewModel employeeVM = new EmployeeUpdateViewModel();
            employeeVM.Name = employee.EmployeeName;
            employeeVM.PageTitle = "Edit employee's details";
            employeeVM.ImagePath = employee.ImagePath;
            employeeVM.Id = (int)employee.EmployeeId;

            
            return View(employeeVM);
        }

        
        [Route("Edit/{id}")]
        [HttpPost]
        public IActionResult Edit(EmployeeUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = "avatar.png";
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }


                EmployeeDTO employee = new EmployeeDTO
                {
                    EmployeeId = model.Id,
                    EmployeeName = model.Name,
                    RoleId = model.Role,
                    ImagePath = "/img/" + uniqueFileName
                };
                EmployeeDTO newEmployee = _employeeRepository.Update(employee);
                return RedirectToAction("Details", new { id = newEmployee.EmployeeId });
            }
            else
            {
                return View();
            }

        }
    }
}
