using AutoMapper;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Models;
using EcommerceApp.MVC.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            try 
            {
                var products = await _productService.GetAllAsync();
                var productViewModels = new List<ProductViewModel>();

                if (products.Any())
                {
                    productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products).ToList();
                }

                return View(productViewModels);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel createProduct, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    if (file != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // set the filename to a random new guid
                        string productPath = Path.Combine(wwwRootPath, @"images\product"); // www root folder, images

                        // save file
                        using (var stream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        };

                        // set view model image url
                        createProduct.Product.ImageUrl = @"\images\product\" + fileName;
                    }

                    var productDto = _mapper.Map<ProductDto>(createProduct.Product);

                    // call our service
                    await _productService.AddAsync(productDto);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }
    }
}
