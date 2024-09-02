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
                    var imageUrl = await _imageHelper.UploadImageAsync(file, @"images\category");

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
        
        [HttpGet]
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



    }
}