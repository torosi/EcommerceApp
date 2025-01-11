using AutoMapper;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Helpers.Interfaces;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductType;
using EcommerceApp.MVC.Models.ProductVariationOption;
using EcommerceApp.MVC.Models.ShoppingCart;
using EcommerceApp.MVC.Models.Sku;
using EcommerceApp.MVC.Models.VariationType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace EcommerceApp.MVC.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICategoryService _categoryService;
        private readonly IProductTypeService _productTypeService;
        private readonly IVariationTypeService _variationTypeService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;
        private readonly ISkuRepository _skuRepository;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ImageHelper imageHelper,
            IShoppingCartService shoppingCartService,
            ICategoryService categoryService,
            IProductTypeService productTypeService,
            IVariationTypeService variationTypeService,
            IUserHelper userHelper,
            ISkuRepository skuRepository
        )
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
            _shoppingCartService = shoppingCartService;
            _categoryService = categoryService;
            _productTypeService = productTypeService;
            _variationTypeService = variationTypeService;
            _userHelper = userHelper;
            _skuRepository = skuRepository;
        }

        public async Task<IActionResult> Index()
        {
            try 
            {
                var products = await _productService.GetAllAsync(includeProperties: "Category,ProductType");
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult Create()
        {
            try
            {                
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel createProduct, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (!_imageHelper.IsImageFile(file))
                        {
                            // Add an error to the ModelState
                            ModelState.AddModelError("File", "The uploaded file is not a valid image.");
                            // Return the view with the current model to show errors
                            return View(createProduct);
                        }

                        var imageUrl = await _imageHelper.UploadImageAsync(file, "product");

                        // set view model image url
                        createProduct.ImageUrl = imageUrl;
                    }

                    var productModel = _mapper.Map<ProductModel>(createProduct);

                    // call our service
                    var newProductModel = await _productService.AddAsync(productModel);

                    return RedirectToAction("Variations", new { productId = newProductModel.Id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try 
            {
                // Get the product we want to edit
                var productModel = await _productService.GetFirstOrDefaultAsync(x => x.Id == id);
                // Get all categories for the dropdown
                var categoryModels = await _categoryService.GetAllAsync();
                // Get all producttypes for the dropdown
                var productTypeModels = await _productTypeService.GetAllAsync();

                // Create a new EditProductViewModel so we can return to view
                var editProductViewModel = new EditProductViewModel();

                // Map DTO to view models
                if (productModel != null)
                    editProductViewModel.Product = _mapper.Map<ProductViewModel>(productModel);
                
                if (categoryModels != null)
                    editProductViewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryModels);
                
                if (productTypeModels != null)
                    editProductViewModel.ProductTypes = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeModels);

                return View(editProductViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product, IFormFile? file)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    var productModel = _mapper.Map<ProductModel>(product);
                    
                    if (productModel != null)
                    {

                        // 1) check if there is a new file and upload/delete new/old images
                        if (file != null) // there is already an existing image and we need to upload a new one
                        {
                            if (!_imageHelper.IsImageFile(file))
                            {
                                ModelState.AddModelError("File", "The uploaded file is not a valid image.");
                                return View(product);
                            }

                            // check if there is already an image, if so then we need to remove it first and then upload the next one
                            if (!string.IsNullOrEmpty(productModel.ImageUrl)) // there is already an image so we need to delete it
                            {
                                var isDeleted = _imageHelper.DeleteImage(productModel.ImageUrl);
                                
                                // what should we do if the image couldnt be deleted?
                                //if (!isDeleted)
                                //{
                                //    throw new Exception("Failed uploading new image - Unable to delete old image");
                                //}
                            }

                            // upload new image
                            var imageUrl = await _imageHelper.UploadImageAsync(file, "product");
                            productModel.ImageUrl = imageUrl;
                        }

                        // 3) Save changes
                        await _productService.UpdateAsync(productModel);

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(product);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var productModel = await _productService.GetFirstOrDefaultAsync(x => x.Id == id);

                if (productModel != null)
                {
                    var productViewModel = _mapper.Map<ProductViewModel>(productModel);

                    return View(productViewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel product)
        {
            try
            {
                var productFromDb = await _productService.GetFirstOrDefaultAsync(x => x.Id == product.Id, tracked: false);

                if (productFromDb == null)
                {
                    ModelState.AddModelError(string.Empty, "The category could not be found.");
                    return View(product);
                }

                if (productFromDb.ImageUrl != null)
                {
                    var isDeleted = _imageHelper.DeleteImage(productFromDb.ImageUrl);
                }

                await _productService.RemoveAsync(productFromDb);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ModelState.AddModelError(string.Empty, "An error occurred while attempting to delete the category.");
                return View(product);
            }
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int productId)
        {
            try
            {
                // 1) Retrieve the product
                var productModel = await _productService.GetFirstOrDefaultAsync(x => x.Id == productId);

                // 2) Retrieve all SKUs and their variations for the product
                var skuWithVariations = await _productService.GetProductVariationsAsync(productId);

                if (productModel != null && skuWithVariations.Any())
                {
                    // 3) Map the product DTO to the product view model
                    var productViewModel = _mapper.Map<ProductViewModel>(productModel);

                    // Populate the options (all variation options across all SKUs) - we dont need this for anything other than populating the drop down at the moment
                    var options = skuWithVariations
                        .SelectMany(sku => sku.VariationOptions.Select(option => new ProductVariationOptionViewModel
                        {
                            VariationTypeId = option.VariationTypeId,
                            VariationTypeName = option.VariationTypeName,
                            VariationValue = option.VariationValue
                        }))
                        .ToList();

                    // Create a grouped dictionary of variation types and their values
                    var groupedVariations = options
                        .GroupBy(option => option.VariationTypeName) // Group by VariationTypeName
                        .ToDictionary(
                            group => group.Key, // The key is the variation type name (e.g., "Size", "Color")
                            group => group.Select(option => option.VariationValue).Distinct().ToList() // Get distinct values for each type
                        );
                    
                    var skuWithVariationsViewModels = skuWithVariations.Select(sku => new SkuWithVariationsViewModel
                    {
                        Sku = new SkuViewModel
                        {
                            Id = sku.SkuId,
                            SkuString = sku.SkuString,
                            Quantity = sku.Quantity,
                            ProductId = sku.ProductId
                        },
                        VariationOptions = sku.VariationOptions.Select(option => _mapper.Map<ProductVariationOptionViewModel>(option)).ToList(),
                        VariationOptionsString = string.Join(",", sku.VariationOptions.OrderBy(x => x.VariationTypeId).Select(y => y.VariationValue))
                    }).ToList();

                    var productDetailsViewModel = new ProductDetailsViewModel()
                    {
                        Product = productViewModel,
                        GroupedVariations = groupedVariations,
                        SkusWithVariations = skuWithVariationsViewModels
                    };

                    return View(productDetailsViewModel);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with proper logging)
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Variations(int productId)
        {
            try 
            {
                // 1) get the product from the db
                var productModel = await _productService.GetFirstOrDefaultAsync(x => x.Id == productId);
                if (productModel == null) return RedirectToAction("Index");

                // map to view model
                var productViewModel = _mapper.Map<ProductViewModel>(productModel);

                // 2) get the product type and fetch all of the linked variation types
                var variationTypeModels = await _variationTypeService.GetAllByProductTypeAsync(productModel.ProductTypeId);

                // map to view model
                var variationTypeViewModels = variationTypeModels.Select(x => new VariationTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                });

                // TODO - We now need to get all of the existing product variation options based on this product and then pass them back to the view as the Input model on the below view model
                // we need a collection of.... 1 sku and a collection of variations... in this format ProductVariationOptionInputViewModel
                var skuWithVariationModels = await _productService.GetProductVariationsAsync(productId);

                var skuWithVariationViewModels = skuWithVariationModels.Select(x => new ProductVariationOptionInputViewModel()
                {
                    Sku = new SkuViewModel()
                    {
                        Id = x.SkuId,
                        Quantity = x.Quantity,
                        SkuString = x.SkuString,
                        ProductId = x.ProductId
                    },
                    VariationValues = x.VariationOptions.Select(y => new VariationValueInputViewModel()
                    {
                        Id = y.Id, // this is the productvariaitionoption id so we can track which ones to add
                        VariationTypeId = y.VariationTypeId,
                        Value = y.VariationValue,
                    }).ToList()
                });

                // 3) create view model for page
                var createViewModel = new CreateProductVariationOptionViewModel()
                {
                    ProductId = productModel.Id,
                    VariationTypes = variationTypeViewModels.ToList(),
                    Input = skuWithVariationViewModels.ToList()
                };

                // 3) pass to view
                return View(createViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Variations(CreateProductVariationOptionViewModel createProductVariationOptionViewModel)
        {
            try
            {
                var mappedOptions = new List<ProductVariationOptionInputModel>();
                var mappedSkus = new List<SkuModel>();

                // TODO: we need to consider variations that already exist, to do that we need to pass some values into the page in above method

                var areVariationsUnique = AreVariationsUnique(createProductVariationOptionViewModel.Input);

                if (!areVariationsUnique)
                {
                    ModelState.AddModelError("", "Duplicate variation value combinations detected. Each variation combination must be unique.");
                    return View(createProductVariationOptionViewModel);
                }

                foreach (var input in createProductVariationOptionViewModel.Input)
                {
                    var sku = new SkuModel()
                    { // TODO - This needs the sku id on it so that we are not accidentially adding ones that already exist again
                        Id = input.Sku.Id,
                        SkuString = input.Sku.SkuString,
                        Quantity = input.Sku.Quantity,
                        ProductId = createProductVariationOptionViewModel.ProductId,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    mappedSkus.Add(sku);
                    
                    foreach (var variationValue in input.VariationValues)
                    {
                        var option = new ProductVariationOptionInputModel
                        {
                            // this also needs the id on it so that we can track ones that already exist. this will mean that when we get them we also need to bring down the id
                            // this might make some of the view models redundant? but worry about that later.
                            // dont forget to add the id field to the views as hidden.
                            Id = variationValue.Id,
                            SkuString = input.Sku.SkuString,
                            VariationTypeId = variationValue.VariationTypeId,
                            VariationValue = variationValue.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        mappedOptions.Add(option);
                    }
                }

                if (mappedSkus.Any() && mappedOptions.Any())
                {
                    await _productService.CreateProductVariations(mappedSkus, mappedOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }


        private bool AreVariationsUnique(IEnumerable<ProductVariationOptionInputViewModel> inputs)
        {
            var combinations = new List<string>();

            foreach (var input in inputs)
            {
                var combination = input.VariationValues // ["Large", "Black"]
                    .OrderBy(x => x.VariationTypeId)
                    .Select(x => x.Value)
                    .ToArray();

                var combinationKey = string.Join(",", combination); // "Large,Black"

                // if the combination already exists in combinations list then we want to add it to duplicates
                if (combinations.Contains(combinationKey))
                {
                    return false;
                }
                else
                {
                    combinations.Add(combinationKey);
                }

            }

            return true;
        }

    }
}
