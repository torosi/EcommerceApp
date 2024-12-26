using AutoMapper;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Helpers.Interfaces;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper, IUserHelper userHelper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userHelper.GetUserId();

                var shoppingCartDtos = await _shoppingCartService.GetShoppingCartByUserAsync(userId);

                if (shoppingCartDtos == null) return View();

                var shoppingCartViewModels = shoppingCartDtos.Select(x => new ShoppingCartViewModel()
                {
                    Id = x.Id,
                    Count = x.Count, 
                    //Product = _mapper.Map<ProductViewModel>(x.Product)
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

                await _shoppingCartService.UpdateAsync(shoppingCartDto);

                return Json(new
                {
                    success = true,
                    count = shoppingCartDto.Count,
                    totalPrice = 0//shoppingCartDto.Count * shoppingCartDto.Product.Price
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

                await _shoppingCartService.UpdateAsync(shoppingCartDto);

                return Json(new
                {
                    success = true,
                    count = shoppingCartDto.Count,
                    totalPrice = 0//shoppingCartDto.Count * shoppingCartDto.Product.Price
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
                var success = await _shoppingCartService.RemoveAsync(cartId);
                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false });
            }
        }

        [HttpGet("ShoppingCart/GetShoppingCartCountAsync")]
        public async Task<IActionResult> GetShoppingCartCountAsync()
        {
            try
            {
                var userId = _userHelper.GetUserId();
                if (userId == null) return Json(new { data = 0 });

                var data = await _shoppingCartService.GetShoppingCartCountByUserAsync(userId);

                return Json(new { data = data });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { data = 0 });
            }
        }

        #endregion
    }
}
