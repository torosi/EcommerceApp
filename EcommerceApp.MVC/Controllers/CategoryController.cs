using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.MVC.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ImageHelper _imageHelper;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper, ImageHelper imageHelper)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // if there are no categories we still want to display the page? so i am still passing in view models even if empty
            var categoryViewModels = new List<CategoryViewModel>();

            try 
            {
                var categoryDtos = await _categoryService.GetAllAsync();

                if (categoryDtos.Any())
                {
                    categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDtos).ToList();
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
            }

            // if no category found, then redirect to index page
            return RedirectToAction(nameof(Index));
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
                        // Add an error to the ModelState
                        ModelState.AddModelError("File", "The uploaded file is not a valid image.");
                        // Return the view with the current model to show errors
                        return View(categoryViewModel);
                    }

                    var imageUrl = await _imageHelper.UploadImageAsync(file, "category");

                    categoryViewModel.ImageUrl = imageUrl;
                }

                var categoryDto = _mapper.Map<CategoryDto>(categoryViewModel);

                await _categoryService.AddAsync(categoryDto);
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
                var categoryDto = await _categoryService.GetFirstOrDefaultAsync(x => x.Id == categoryId);

                if (categoryDto != null)
                {
                    var cateogryViewModel = _mapper.Map<CategoryViewModel>(categoryDto);
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
                    var categoryFromDb = await _categoryService.GetFirstOrDefaultAsync(x => x.Id == category.Id);

                    // 2) update category
                    if (categoryFromDb != null)
                    {
                        // check if we need to upload a new image
                        if (file != null) // there is already an existing image and we need to upload a new one
                        {
                            // check if there is already an image, if so then we need to remove it first and then upload the next one
                            if (!string.IsNullOrEmpty(categoryFromDb.ImageUrl)) // there is already an image so we need to delete it
                            {
                                var isDeleted = _imageHelper.DeleteImage(categoryFromDb.ImageUrl);
                                // what should we do if the image couldnt be deleted?
                                if (!isDeleted)
                                {
                                    throw new Exception("Failed uploading new image - Unable to delete old image");
                                }
                            }

                            // upload new image
                            var imageUrl = await _imageHelper.UploadImageAsync(file, "category");

                            categoryFromDb.ImageUrl = imageUrl;
                        }

                        categoryFromDb.Name = category.Name;
                        categoryFromDb.Description = category.Description;

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
        public async Task<IActionResult> Details(int categoryId)
        {
            try
            {
                var categoryDto = await _categoryService.GetFirstOrDefaultAsync(x => x.Id==categoryId);

                if (categoryDto == null)
                {
                    return NotFound();
                }

                var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryDto);

                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var categoryDto = await _categoryService.GetFirstOrDefaultAsync(x => x.Id == id);
               
                if (categoryDto == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryDto);
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
                var categoryFromDb = await _categoryService.GetFirstOrDefaultAsync(x => x.Id == category.Id, tracked: false);
                if (categoryFromDb == null)
                {
                    ModelState.AddModelError(string.Empty, "The category could not be found.");
                    return View(category);
                }

                if (categoryFromDb.ImageUrl != null)
                {
                    var isDeleted = _imageHelper.DeleteImage(categoryFromDb.ImageUrl);

                    //if (!isDeleted) // if the image could not be deleted then we want to return out, otherwise we will end up with load of undeleted images
                    //{
                    //    ModelState.AddModelError(string.Empty, "Failed to delete the associated image.");
                    //    return View(category);
                    //}
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
                var categoryDtos = await _categoryService.GetAllAsync();

                // Handle null or empty list
                if (categoryDtos == null || !categoryDtos.Any())
                {
                    return Json(new { success = false, data = new List<CategoryViewModel>() });
                }

                var categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDtos);

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