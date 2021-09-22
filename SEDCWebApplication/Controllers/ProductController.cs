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
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        

        public ProductController(IProductRepository productRepository, IWebHostEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;


        }

        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {

            List<ProductDTO> products = _productRepository.GetAllProducts().ToList();
            ViewBag.Title = "Products";

            return View(products);
        }

        [HttpGet]
        [Route("DetailsView/{id}")]
        public IActionResult Details(int id)
        {

            ProductDTO product = _productRepository.GetProductById(id);


            ProductDetailsViewModel productVM = new ProductDetailsViewModel();
            productVM.PageTitle = "Product's details";

            productVM.ProductName = product.ProductName;
            productVM.UnitPrice = product.UnitPrice;
            productVM.IsDiscounted = (bool)product.IsDiscounted;
            productVM.IsActive = (bool)product.IsActive;
            productVM.IsDeleted = (bool)product.IsDeleted;
            productVM.Size = product.Size;
            productVM.ImagePath = product.ImagePath;

            return View(productVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
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


                ProductDTO product = new ProductDTO
                {
                    ProductId = null,
                    ProductName = model.ProductName,
                    UnitPrice = model.UnitPrice,
                    IsDiscounted = model.IsDiscounted,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Size = model.Size,
                    ImagePath = "/img/" + uniqueFileName
                };
                ProductDTO newProduct = _productRepository.Add(product);
                return RedirectToAction("Details", new { id = newProduct.ProductId });
            } else
            {
                return View();
            }
            
        }
    }
}
