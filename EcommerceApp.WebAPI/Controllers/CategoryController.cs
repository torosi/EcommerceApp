using AutoMapper;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.Service.Contracts;
using EcommerceApp.WebAPI.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occured while fetching categories");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"An error occured while fetching category with id {id}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryDto category)
        {
            try
            {
                var categoryModel = _mapper.Map<CategoryModel>(category);
                await _categoryService.UpdateAsync(categoryModel);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"An error occured while updating category");
            }
        }


    }
}
