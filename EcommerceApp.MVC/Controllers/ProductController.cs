using AutoMapper;
using EcommerceApp.Domain.Constants;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ImageHelper _imageHelper;

        public ProductController(IProductService productService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ImageHelper imageHelper)
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
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
                        if (!_imageHelper.IsImageFile(file)) //TODO: THIS NEED TESTING
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

        [HttpGet]
        public async  Task<IActionResult> Delete(int id)
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

    }
}
