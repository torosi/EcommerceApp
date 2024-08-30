using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceApp.Domain.Services.Contracts;
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

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
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


    }
}