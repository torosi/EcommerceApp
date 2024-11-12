using AutoMapper;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.Domain.Services.Implementations;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Security.Claims;
using System.Web;

namespace EcommerceApp.MVC.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICategoryService _categoryService;
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ImageHelper _imageHelper;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ImageHelper imageHelper,
            IShoppingCartService shoppingCartService,
            ICategoryService categoryService,
            IProductTypeService productTypeService
        )
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
            _shoppingCartService = shoppingCartService;
            _categoryService = categoryService;
            _productTypeService = productTypeService;
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

                    var productDto = _mapper.Map<ProductDto>(createProduct);

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
                // 1) gets the product
                // 2) gets all of this products variations
                var productDto = await _productService.GetFirstOrDefaultAsync(x => x.Id == productId);
                var productOptions = await _productService.GetProductVariationsAsync(productId);

                if (productDto != null && productOptions.Any())
                {
                    var productViewModel = _mapper.Map<ProductViewModel>(productDto);

                    // 3) Map to SkuViewModel which will have a list of variations on it.
                    // we get a sku row for each variation option - here we want to group all of these rows into one list per sku
                    productViewModel.Options = productOptions
                    .Select(option => new ProductVariationOptionViewModel
                    {
                        SkuId = option.SkuId,
                        SkuString = option.SkuString,
                        Quantity = option.Quantity,
                        VariationTypeName = option.VariationTypeName,  // e.g., "Size" or "Color"
                        VariationValueName = option.VariationValueName // e.g., "Small" or "Red"
                    })
                    .ToList();

                    // 4) we need to group the variations inside each of these sku items so that we can get a list of what variation types we have
                    // using this we can display a drop down for each variation type like size and colour
                    // SelectMany takes each SKU and extracts its variations, combining all variations from all SKUs into a single flat collection(instead of keeping them in separate lists for each SKU).
                    productViewModel.GroupedVariations = productViewModel.Options
                        // no need to select many anymore as i changed view model. it was a list but it was only ever going to have one variation per sku row so i have added jsut one item so we dont need to flatten. im going to leave here as it might be useful later
                        //.SelectMany(sku => sku.Variation)  // Flatten the variations across all SKUs
                        .GroupBy(v => v.VariationTypeName)  // Group by VariationType so that we then have a list of all of the possible values
                        .ToDictionary(
                            group => group.Key,  // Group by VariationType
                            group => group.Select(v => v.VariationValueName).Distinct().ToList() // Get distinct variation values
                        ); // here we have a dictionary of the variation type and all of the values - perfect for a drop down.

                    // we are returning a shoppingcartitem because that is what will potentially be saved to the db
                    var shoppingCartViewModel = new ShoppingCartViewModel()
                    {
                        Product = productViewModel,
                        Count = 1 // how many items do we want to add. eventually user needs to set this
                    };

                    return View(shoppingCartViewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(ShoppingCartViewModel cart)
        {
            try
            {
                // 1) we need the currently logged in user
                var claimsIdentity = (ClaimsIdentity)User.Identity;

                if (claimsIdentity == null) throw new Exception("User could not be found");

                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userId == null) throw new Exception("User could not be found");

                // 2) if there is already a cart item for this product then we want to add the product to the existing one
                var cartFromDb = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.ProductId == cart.Product.Id, tracked: false);

                if (cartFromDb != null)
                {
                    cartFromDb.Count += cart.Count;
                    await _shoppingCartService.UpdateAsync(cartFromDb);
                }
                else // 3) if there is no product already then we can just add the new item to cart
                {
                    var shoppingCartDto = new ShoppingCartDto()
                    { 
                        ProductId = cart.Product.Id,
                        ApplicationUserId = userId,
                        Count = cart.Count,
                        Id = cart.Id
                    };

                    await _shoppingCartService.AddAsync(shoppingCartDto);
                }

                //return Redirect(Request.Headers["Referer"].ToString()); // takes you to the previous page
                return RedirectToAction("Index", "ShoppingCart"); // change to shopping cart
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
