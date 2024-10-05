using AutoMapper;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceApp.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {

            try
            {
                // 1) we need the currently logged in user
                var claimsIdentity = (ClaimsIdentity)User.Identity;

                if (claimsIdentity == null) throw new Exception("User could not be found");

                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userId == null) throw new Exception("User could not be found");

                var shoppingCartDtos = await _shoppingCartService.GetShoppingCartByUser(userId);

                if (shoppingCartDtos == null) return View();

                var shoppingCartViewModels = shoppingCartDtos.Select(x => new ShoppingCartViewModel()
                {
                    Count = x.Count, 
                    Product = _mapper.Map<ProductViewModel>(x.Product)
                });

                return View(shoppingCartViewModels);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View();
        }
    }
}
