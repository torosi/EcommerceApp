using AutoMapper;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.MVC.Models.ProductType;
using EcommerceApp.MVC.Models.VariationType;
using EcommerceApp.Service.Contracts;
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
        private readonly IVariationTypeService _variationTypeService;

        public ProductTypeController(IProductTypeService productTypeService, IMapper mapper, ILogger<ProductTypeController> logger, IVariationTypeService variationTypeService)
        {
            _productTypeService = productTypeService;
            _mapper = mapper;
            _logger = logger;
            _variationTypeService = variationTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var productTypeViewModels = new List<ProductTypeViewModel>();

            try 
            {
                // get all product types
                var productTypeModels = await _productTypeService.GetAllAsync();

                // map to view models
                if (productTypeModels.Any())
                {
                    productTypeViewModels = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeModels).ToList();
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return View(productTypeViewModels);
        }
        

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var variationTypes = await _variationTypeService.GetAllAsync();

                var createProductTypeViewModel = new CreateProductTypeViewModel()
                {
                    ProductType = new ProductTypeViewModel(),
                    VariationTypes = _mapper.Map<IList<CreateVariationTypeViewModel>>(variationTypes)
                };

                return View(createProductTypeViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductTypeViewModel createProductType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map product type view model to dto
                    var productModel = _mapper.Map<ProductTypeModel>(createProductType.ProductType);

                    // save product type view model
                    var productTypeId = await _productTypeService.AddAsync(productModel);

                    var variationTypeIds = createProductType.VariationTypes
                        .Where(x => x.IsSelected && x.Id != 0)
                        .Select(x => x.Id);

                    if (variationTypeIds.Any())
                    {
                        await _variationTypeService.CreateProductTypeAndVariationTypeMappings(variationTypeIds, productTypeId);
                    }
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
                var productTypeModel = await _productTypeService.GetProductTypeByIdAsync(productTypeId);

                if (productTypeModel != null)
                {
                    var productTypeVM = _mapper.Map<ProductTypeViewModel>(productTypeModel);
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
                    var productTypeModel = _mapper.Map<ProductTypeModel>(productType);

                    // update product type
                    await _productTypeService.UpdateAsync(productTypeModel);
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
                var productTypeModel = await _productTypeService.GetProductTypeByIdAsync(productTypeId);

                // delete the product type
                if (productTypeModel != null)
                {
                    var productTypeVM = _mapper.Map<ProductTypeViewModel>(productTypeModel);
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
                var productFromDb = await _productTypeService.GetProductTypeByIdAsync(productType.Id);

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
                var productTypeModels = await _productTypeService.GetAllAsync();

                if (productTypeModels == null || !productTypeModels.Any())
                {
                    return Json(new { success = false, data = new List<ProductTypeViewModel>() });
                }

                var productTypeViewModels = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeModels);

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
