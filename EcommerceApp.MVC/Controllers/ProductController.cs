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
        public async Task<IActionResult> Create(CreateProductViewModel createProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createProduct != null)
                    {

                        if (createProduct.UploadImage != null && createProduct.UploadImage.ImageFile != null)
                        {
                            // Read the file's content into the ImageData property
                            using (var memoryStream = new MemoryStream())
                            {
                                createProduct.UploadImage.ImageFile.CopyTo(memoryStream);
                                createProduct.Product.Image.ImageData = memoryStream.ToArray();
                            }
                        }

                        var productDto = _mapper.Map<ProductDto>(createProduct.Product);

                        // call our service
                        await _productService.AddAsync(productDto);
                    }
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
