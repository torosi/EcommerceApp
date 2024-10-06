using AutoMapper;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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
                    Id = x.Id,
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

        


        #region API Calls

        [HttpPost]
        public async Task<IActionResult> Increment(int cartId)
        {
            try
            {
                // 1) get the cart from the db
                var shoppingCartDto = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.Id == cartId, tracked: false);

                // 2) increment, update and then return json
                if (shoppingCartDto == null) return Json(new { success = false });

                shoppingCartDto.Count++;

                await _shoppingCartService.Update(shoppingCartDto);

                return Json(new
                {
                    success = true,
                    count = shoppingCartDto.Count,
                    totalPrice = shoppingCartDto.Count * shoppingCartDto.Product.Price
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Decrement(int cartId)
        {
            try
            {
                var shoppingCartDto = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.Id == cartId, tracked: false);

                if (shoppingCartDto == null) return Json(new { success = false });

                shoppingCartDto.Count--;

                await _shoppingCartService.Update(shoppingCartDto);

                return Json(new
                {
                    success = true,
                    count = shoppingCartDto.Count,
                    totalPrice = shoppingCartDto.Count * shoppingCartDto.Product.Price
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false });
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int cartId)
        {
            try
            {
                var success = await _shoppingCartService.Remove(cartId);
                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false });
            }
        }

        #endregion
    }
}
