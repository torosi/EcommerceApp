using AutoMapper;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.Domain.Services.Implementations;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Web;

namespace EcommerceApp.MVC.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ImageHelper _imageHelper;

        public ProductController(IProductService productService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ImageHelper imageHelper, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
            _shoppingCartService = shoppingCartService;
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel createProduct, IFormFile? file)
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
                        createProduct.Product.ImageUrl = imageUrl;
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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
                var productDto = await _productService.GetFirstOrDefaultAsync(x => x.Id == productId);
                if (productDto != null)
                {
                    var productViewModel = _mapper.Map<ProductViewModel>(productDto);
                    var shoppingCartViewModel = new ShoppingCartViewModel()
                    {
                        Product = productViewModel,
                        Count = 1
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
                    await _shoppingCartService.Update(cartFromDb);
                }
                else // 3) if there is no product already then we can just add the new item to cart
                {
                    var shoppingCartDto = new ShoppingCartDto()
                    { 
                        ProductId = cart.Product.Id,
                        ApplicationUserId = userId,
                        Count = cart.Count
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
