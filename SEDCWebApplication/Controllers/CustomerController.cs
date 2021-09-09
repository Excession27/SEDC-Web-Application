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
    [Route("Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        

        public CustomerController(ICustomerRepository customerRepository, IWebHostEnvironment hostingEnvironment)
        {
            _customerRepository = customerRepository;
            _hostingEnvironment = hostingEnvironment;


        }


        [Route("List")]
        public IActionResult List()
        {

            List<CustomerDTO> customers = _customerRepository.GetAllCustomers().ToList();
            ViewBag.Title = "Customers";

            return View(customers);
        }

        [Route("DetailsView/{id}")]
        public IActionResult Details(int id)
        {

            CustomerDTO customer = _customerRepository.GetCustomerById(id);


            CustomerDetailsViewModel customerVM = new CustomerDetailsViewModel();
            customerVM.CustomerName = customer.Name;
            customerVM.PageTitle = "Customer's details";

            return View(customerVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateViewModel model)
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


                CustomerDTO customer = new CustomerDTO
                {
                    Id = null,
                    Name = model.Name,
                    ContactId = model.ContactId,
                    ImagePath = "/img/" + uniqueFileName
                };
                CustomerDTO newCustomer = _customerRepository.Add(customer);
                return RedirectToAction("Details", new { id = newCustomer.Id });
            } else
            {
                return View();
            }
            
        }
    }
}
