using AutoMapper;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel createProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createProduct != null)
                    {
                        var productDto = _mapper.Map<ProductDto>(createProduct.product);
                        await _productService.AddAsync(productDto);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }
    }
}
