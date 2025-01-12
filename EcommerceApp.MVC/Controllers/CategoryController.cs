using AutoMapper;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ImageHelper _imageHelper;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper, ImageHelper imageHelper, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
            _imageHelper = imageHelper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // if there are no categories we still want to display the page? so i am still passing in view models even if empty
            var categoryViewModels = new List<CategoryViewModel>();

            try 
            {
                var categoryModels = await _categoryService.GetAllAsync();

                if (categoryModels.Any())
                {
                    categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryModels).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return View(categoryViewModels);
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
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    if (!_imageHelper.IsImageFile(file))
                    {
                        ModelState.AddModelError("File", "The uploaded file is not a valid image.");
                        return View(categoryViewModel);
                    }

                    var imageUrl = await _imageHelper.UploadImageAsync(file, "category");

                    categoryViewModel.ImageUrl = imageUrl;
                }

                var categoryModel = _mapper.Map<CategoryModel>(categoryViewModel);

                await _categoryService.AddAsync(categoryModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int categoryId)
        {
            try 
            {
                var categoryModel = await _categoryService.GetCategoryById(categoryId);

                if (categoryModel != null)
                {
                    var cateogryViewModel = _mapper.Map<CategoryViewModel>(categoryModel);
                    return View(cateogryViewModel);
                } 

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(CategoryViewModel category, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // 1) get the category from db
                    var categoryFromDb = _mapper.Map<CategoryModel>(category);

                    // 2) update category
                    if (categoryFromDb != null)
                    {
                        // check if we need to upload a new image
                        if (file != null) // there is already an existing image and we need to upload a new one
                        {
                            if (!_imageHelper.IsImageFile(file))
                            {
                                ModelState.AddModelError("File", "The uploaded file is not a valid image.");
                                return View(category);
                            }

                            if (!string.IsNullOrEmpty(categoryFromDb.ImageUrl)) // there is already an image so we need to delete it
                            {
                                var isDeleted = _imageHelper.DeleteImage(categoryFromDb.ImageUrl);
                                // what should we do if the image couldnt be deleted?
                                //if (!isDeleted)
                                //{
                                //    throw new Exception("Failed uploading new image - Unable to delete old image");
                                //}
                            }

                            // upload new image
                            var imageUrl = await _imageHelper.UploadImageAsync(file, "category");
                            categoryFromDb.ImageUrl = imageUrl;
                        }

                        // 3) save changes
                        await _categoryService.UpdateAsync(categoryFromDb);

                        // 4) redirect to index page
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // if not updated successfully then go back to view
            return View(category);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int categoryId, int page = 1, int itemsPerPage = 20)
        {
            try
            {
                var categoryModel = await _categoryService.GetCategoryById(categoryId);

                if (categoryModel != null)
                {
                    // Get products for this category
                    var productResult = await _productService.SearchProductByCategoryId(categoryId, pageNumber: page, itemsPerPage: itemsPerPage);

                    var productViewModels = new List<ProductViewModel>();
                    if (productResult.Products.Any())
                    {
                        productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(productResult.Products).ToList();
                    }

                    var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryModel);

                    var viewCategoryViewModel = new ViewCategoryViewModel()
                    {
                        Category = categoryViewModel, 
                        Products = productViewModels,
                        TotalPages = (int)Math.Ceiling((double)productResult.TotalCount / itemsPerPage),
                        ItemsPerPage = itemsPerPage,
                        CurrentPage = page
                    };

                    return View(viewCategoryViewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var categoryModel = await _categoryService.GetCategoryById(id);
               
                if (categoryModel == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryModel);
                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(CategoryViewModel category)
        {
            try
            {
                var categoryFromDb = await _categoryService.GetCategoryById(category.Id);
                if (categoryFromDb == null)
                {
                    ModelState.AddModelError(string.Empty, "The category could not be found.");
                    return View(category);
                }

                if (categoryFromDb.ImageUrl != null)
                {
                    var isDeleted = _imageHelper.DeleteImage(categoryFromDb.ImageUrl);
                }

                await _categoryService.RemoveAsync(categoryFromDb);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ModelState.AddModelError(string.Empty, "An error occurred while attempting to delete the category.");
                return View(category);
            }
        }


        #region API CALLS

        // 1) get the cateogry from db
        // 2) if null then return out
        // 3) if there is an image then will need to remove this
        // 4) remove cateogry from db
        // 5) return out
        /*[HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            try 
            {
                var categoryFromDb = await _categoryService.GetFirstOrDefaultAsync(x => x.Id == categoryId);
                if (categoryFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting - category not found" });
                }

                if (categoryFromDb.ImageUrl != null)
                {
                    var isDeleted = _imageHelper.DeleteImage(categoryFromDb.ImageUrl);

                    if (!isDeleted) // if the image could not be deleted then we want to return out, otherwise we will end up with load of undeleted images
                    {
                        return Json(new { success = false, message = "Error while deleting - could not delete image" });
                    }
                }

                await _categoryService.RemoveAsync(categoryFromDb);

                return Json(new { success = true, message = "Delete Successful" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }*/

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try 
            {
                var categoryModels = await _categoryService.GetAllAsync();

                // Handle null or empty list
                if (categoryModels == null || !categoryModels.Any())
                {
                    return Json(new { success = false, data = new List<CategoryViewModel>() });
                }

                var categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryModels);

                return Json(new { success = true, data = categoryViewModels.ToList() });

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