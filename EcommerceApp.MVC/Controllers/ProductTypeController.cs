using AutoMapper;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.Domain.Services.Implementations;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Models.ProductType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
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
            var productTypeViewModels = new List<ProductTypeViewModel>();

            try 
            {
                // get all product types
                var productTypeDtos = await _productTypeService.GetAllAsync();

                // map to view models
                if (productTypeDtos.Any())
                {
                    productTypeViewModels = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeDtos).ToList();
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return View(productTypeViewModels);
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
                Console.WriteLine(ex.Message);
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


        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int productTypeId)
        {
            try
            {
                var productTypeDto = await _productTypeService.GetFirstOrDefaultAsync(x => x.Id == productTypeId);

                if (productTypeDto != null)
                {
                    var productTypeVM = _mapper.Map<ProductTypeViewModel>(productTypeDto);
                    return View(productTypeVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(ProductTypeViewModel productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map product type to dto
                    var productTypeDto = _mapper.Map<ProductTypeDto>(productType);

                    // update product type
                    await _productTypeService.UpdateAsync(productTypeDto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int productTypeId)
        {
            try
            {
                // get product type
                var productTypeDto = await _productTypeService.GetFirstOrDefaultAsync(x => x.Id == productTypeId);

                // delete the product type
                if (productTypeDto != null)
                {
                    var productTypeVM = _mapper.Map<ProductTypeViewModel>(productTypeDto);
                    return View(productTypeVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductType(ProductTypeViewModel productType)
        {
            try
            {
                var productFromDb = await _productTypeService.GetFirstOrDefaultAsync(x => x.Id == productType.Id, tracked: false);

                if (productFromDb == null)
                {
                    ModelState.AddModelError(string.Empty, "The product type could not be found.");
                    return View(productType);
                }

                await _productTypeService.RemoveAsync(productFromDb);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ModelState.AddModelError(string.Empty, "An error occurred while attempting to delete the product type.");
                return View(productType);
            }
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
