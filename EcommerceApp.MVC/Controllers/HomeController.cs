using AutoMapper;
using EcommerceApp.MVC.Models;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categoryModels = await _categoryService.GetAllAsync(6);
                var categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryModels);

                var homeViewModel = new HomePageViewModel();

                if (categoryViewModels.Any())
                {
                    homeViewModel.Categories = categoryViewModels.ToList();
                }

                return View(homeViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
