using AutoMapper;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Helpers.Interfaces;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductVariationOption;
using EcommerceApp.MVC.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcommerceApp.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;
        private readonly ISkuService _skuService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper, IUserHelper userHelper, ISkuService skuService)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _userHelper = userHelper;
            _skuService = skuService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userHelper.GetUserId();

                var shoppingCartModels = await _shoppingCartService.GetShoppingCartByUserAsync(userId);

                if (shoppingCartModels == null) return View();

                var shoppingCartViewModels = shoppingCartModels.Select(x => new ShoppingCartViewModel()
                {
                    Id = x.Id,
                    Count = x.Count, 
                    Product = _mapper.Map<ProductViewModel>(x.Product),
                    Variations = x.Sku.VariationOptions
                        .OrderBy(x => x.VariationTypeId)
                        .Select(x => new ProductVariationOptionViewModel()
                        {
                            VariationTypeId = x.VariationTypeId,
                            VariationTypeName = x.VariationTypeName,
                            VariationValue = x.VariationValue,
                        })
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
                var shoppingCartModel = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.Id == cartId, tracked: false);

                // 2) increment, update and then return json
                if (shoppingCartModel == null) return Json(new { success = false });

                shoppingCartModel.Count++;

                await _shoppingCartService.UpdateAsync(shoppingCartModel);

                return Json(new
                {
                    success = true,
                    count = shoppingCartModel.Count,
                    totalPrice = 0//shoppingCartModel.Count * shoppingCartModel.Product.Price
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
                var shoppingCartModel = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.Id == cartId, tracked: false);

                if (shoppingCartModel == null) return Json(new { success = false });

                shoppingCartModel.Count--;

                await _shoppingCartService.UpdateAsync(shoppingCartModel);

                return Json(new
                {
                    success = true,
                    count = shoppingCartModel.Count,
                    totalPrice = 0//shoppingCartModel.Count * shoppingCartModel.Product.Price
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


        [HttpPost]
        public async Task<IActionResult> AddToCart(string skuString, int count)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                if (userId == null) throw new Exception("User could not be found");

                var skuModel = await _skuService.GetFirstOrDefaultAsync(x => x.SkuString == skuString);
                if (skuModel == null) throw new Exception("Product could not be found");

                // 1) get the current cart from the db to see if the product is already in there
                var cartFromDb = await _shoppingCartService.GetFirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.SkuId == skuModel.Id, tracked: false);
                
                // if this is null we need to add a new record
                if (cartFromDb == null) 
                {
                    var newCartItemModel = new ShoppingCartModel()
                    {
                        SkuId = skuModel.Id,
                        ApplicationUserId = userId,
                        Count = count,
                        Id = 0 // 0 because it is new so does not matter
                    };

                    await _shoppingCartService.AddAsync(newCartItemModel);
                }
                else
                {
                    // if the cart does exist then we only want to increase the quantity
                    cartFromDb.Count += count;
                    await _shoppingCartService.UpdateAsync(cartFromDb);
                }

                return Json(new {success = true, message = "Cart updated successfully"});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new {success = false, message = ex.Message});
            }
        }

        #endregion
    }
}
