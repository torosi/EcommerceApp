using AutoMapper;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductTypeController> _logger;

        public ProductTypeController(IProductTypeService productTypeService, IMapper mapper, ILogger<ProductTypeController> logger)
        {
            _productTypeService = productTypeService;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            try 
            {
                // get all product types
                var productTypeDtos = await _productTypeService.GetAllAsync();

                // map to view models
                if (productTypeDtos.Any())
                {
                    var productTypeViewModels = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeDtos);
                    return View(productTypeViewModels);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        

        [HttpGet("Create")]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductTypeViewModel productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map product type view model to dto
                    var productDto = _mapper.Map<ProductTypeDto>(productType);

                    // save product type view model
                    await _productTypeService.AddAsync(productDto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }


        #region API CALLS

        [HttpGet("GetAllProductTypes")]
        public async Task<IActionResult> GetAllProductTypes()
        {
            try
            {
                _logger.LogInformation("Getting All Product Types");
                var productTypeDtos = await _productTypeService.GetAllAsync();

                if (productTypeDtos == null || !productTypeDtos.Any())
                {
                    return Json(new { success = false, data = new List<ProductTypeViewModel>() });
                }

                var productTypeViewModels = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeDtos);

                return Json(new { success = true, data = productTypeViewModels.ToList() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion
        
    }
}
