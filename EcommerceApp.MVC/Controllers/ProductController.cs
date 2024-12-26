using AutoMapper;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Dtos.Variations;
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
                // var productViewModel = new CreateProductViewModel();
                
                // // get variation types and map to view models
                // var variationTypeDtos = await _variationTypeService.GetAllAsync();
                // var variationTypeViewModels = variationTypeDtos.Select(x => new VariationTypeViewModel()
                //     {
                //         Id = x.Id,
                //         Name = x.Name,
                //         Created = x.Created,
                //         Updated = x.Updated
                //     });
                
                // // set variation types onto view model to pass to view
                // productViewModel.VariationsTypes = variationTypeViewModels;
                
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

                    var productDto = _mapper.Map<ProductDto>(createProduct);

                    // call our service
                    var newProductDto = await _productService.AddAsync(productDto);

                    return RedirectToAction("Variations", new { productId = newProductDto.Id });
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
                var productDto = await _productService.GetFirstOrDefaultAsync(x => x.Id == id);
                // Get all categories for the dropdown
                var categoryDtos = await _categoryService.GetAllAsync();
                // Get all producttypes for the dropdown
                var productTypeDtos = await _productTypeService.GetAllAsync();

                // Create a new EditProductViewModel so we can return to view
                var editProductViewModel = new EditProductViewModel();

                // Map DTO to view models
                if (productDto != null)
                    editProductViewModel.Product = _mapper.Map<ProductViewModel>(productDto);
                
                if (categoryDtos != null)
                    editProductViewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDtos);
                
                if (productTypeDtos != null)
                    editProductViewModel.ProductTypes = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productTypeDtos);

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
                    var productDto = _mapper.Map<ProductDto>(product);
                    
                    if (productDto != null)
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
                            if (!string.IsNullOrEmpty(productDto.ImageUrl)) // there is already an image so we need to delete it
                            {
                                var isDeleted = _imageHelper.DeleteImage(productDto.ImageUrl);
                                
                                // what should we do if the image couldnt be deleted?
                                //if (!isDeleted)
                                //{
                                //    throw new Exception("Failed uploading new image - Unable to delete old image");
                                //}
                            }

                            // upload new image
                            var imageUrl = await _imageHelper.UploadImageAsync(file, "product");
                            productDto.ImageUrl = imageUrl;
                        }

                        // 3) Save changes
                        await _productService.UpdateAsync(productDto);

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
                var productDto = await _productService.GetFirstOrDefaultAsync(x => x.Id == id);

                if (productDto != null)
                {
                    var productViewModel = _mapper.Map<ProductViewModel>(productDto);

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
                var productDto = await _productService.GetFirstOrDefaultAsync(x => x.Id == productId);

                // 2) Retrieve all SKUs and their variations for the product
                var skuWithVariations = await _productService.GetProductVariationsAsync(productId);

                if (productDto != null && skuWithVariations.Any())
                {
                    // 3) Map the product DTO to the product view model
                    var productViewModel = _mapper.Map<ProductViewModel>(productDto);

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
                        VariationOptions = sku.VariationOptions.Select(option => new ProductVariationOptionViewModel
                        {
                            VariationTypeId = option.VariationTypeId,
                            VariationTypeName = option.VariationTypeName,
                            VariationValue = option.VariationValue
                        }).ToList(),
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


        // [Authorize]
        // [HttpPost("AddToCart")]
        // public async Task<IActionResult> AddToCart(ShoppingCartViewModel cart)
        // {
        //     try
        //     {
        //         // 1) we need the currently logged in user
        //         var claimsIdentity = (ClaimsIdentity)User.Identity;

        //         if (claimsIdentity == null) throw new Exception("User could not be found");

        //         var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        //         if (userId == null) throw new Exception("User could not be found");

        //         // 2) if there is already a cart item for this product then we want to add the product to the existing one
        //         var cartFromDb = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.ProductId == cart.Product.Id, tracked: false);

        //         if (cartFromDb != null)
        //         {
        //             cartFromDb.Count += cart.Count;
        //             await _shoppingCartService.UpdateAsync(cartFromDb);
        //         }
        //         else // 3) if there is no product already then we can just add the new item to cart
        //         {
        //             var shoppingCartDto = new ShoppingCartDto()
        //             { 
        //                 ProductId = cart.Product.Id,
        //                 ApplicationUserId = userId,
        //                 Count = cart.Count,
        //                 Id = cart.Id
        //             };

        //             await _shoppingCartService.AddAsync(shoppingCartDto);
        //         }

        //         //return Redirect(Request.Headers["Referer"].ToString()); // takes you to the previous page
        //         return RedirectToAction("Index", "ShoppingCart"); // change to shopping cart
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex);
        //         return RedirectToAction("Index", "Home");
        //     }
        // }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Variations(int productId)
        {
            try 
            {
                // 1) get the product from the db
                var productDto = await _productService.GetFirstOrDefaultAsync(x => x.Id == productId);
                if (productDto == null) return RedirectToAction("Index");

                // map to view model
                var productViewModel = _mapper.Map<ProductViewModel>(productDto);

                // 2) get the product type and fetch all of the linked variation types
                var variationTypeDtos = await _variationTypeService.GetAllByProductTypeAsync(productDto.ProductTypeId);

                // map to view model
                var variationTypeViewModels = variationTypeDtos.Select(x => new VariationTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                });

                // TODO - We now need to get all of the existing product variation options based on this product and then pass them back to the view as the Input model on the below view model
                // we need a collection of.... 1 sku and a collection of variations... in this format ProductVariationOptionInputViewModel
                var skuWithVariationDtos = await _productService.GetProductVariationsAsync(productId);

                var skuWithVariationViewModels = skuWithVariationDtos.Select(x => new ProductVariationOptionInputViewModel()
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
                    ProductId = productDto.Id,
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
                var mappedOptions = new List<ProductVariationOptionInputDto>();
                var mappedSkus = new List<SkuDto>();

                // TODO: we need to consider variations that already exist, to do that we need to pass some values into the page in above method

                var areVariationsUnique = AreVariationsUnique(createProductVariationOptionViewModel.Input);

                if (!areVariationsUnique)
                {
                    ModelState.AddModelError("", "Duplicate variation value combinations detected. Each variation combination must be unique.");
                    return View(createProductVariationOptionViewModel);
                }

                foreach (var input in createProductVariationOptionViewModel.Input)
                {
                    var sku = new SkuDto()
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
                        var option = new ProductVariationOptionInputDto
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


        /// <summary>
        /// Method to determine if there are duplicate variation options in our collection of ProductVariationOptionInputViewModels
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
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


        #region API CALLS

        /// <summary>
        /// Method to add sku item to shopping cart
        /// </summary>
        /// <param name="skuString"></param>
        /// <param name="count"></param>
        /// <returns>Json object</returns>
        [HttpPost]
        public async Task<IActionResult> AddToCart(string skuString, int count)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                if (userId == null) throw new Exception("User could not be found");

                var skuDto = await _skuRepository.GetFirstOrDefaultAsync(x => x.SkuString == skuString);
                if (skuDto == null) throw new Exception("Product could not be found");

                // 1) get the current cart from the db to see if the product is already in there
                var cartFromDb = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.SkuId == 1, tracked: false);
                
                // if this is null we need to add a new record
                if (cartFromDb == null) 
                {
                    var newCartItemDto = new ShoppingCartDto()
                    {
                        SkuId = skuDto.Id,
                        ApplicationUserId = userId,
                        Count = count,
                        Id = 0 // 0 because it is new so does not matter
                    };

                    await _shoppingCartService.AddAsync(newCartItemDto);
                }
                else
                {
                    // if the cart does exist then we only want to increase the quantity
                    cartFromDb.Count += count;
                    await _shoppingCartService.UpdateAsync(cartFromDb);
                }

                return Json(new {success = true, message = "Cart updated successfully"});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new {success = false, message = ex.Message});
            }
        }


        #endregion
    }
}
