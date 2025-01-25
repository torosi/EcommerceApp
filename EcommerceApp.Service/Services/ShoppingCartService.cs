using Microsoft.Extensions.Logging;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.ShoppingCart;
using EcommerceApp.Domain.Extentions.Mappings;

namespace EcommerceApp.Service.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ILogger<ShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task AddAsync(ShoppingCartModel shoppingCart)
        {
            if (shoppingCart == null) throw new ArgumentNullException(nameof(shoppingCart));

            await _shoppingCartRepository.AddAsync(shoppingCart.ToCreateModel());
            await _shoppingCartRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ShoppingCartModel?> GetShoppingCartById(int cartId)
        {
            ArgumentOutOfRangeException.ThrowIfZero(cartId);

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(cartId);
            return shoppingCart;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ShoppingCartModel>> GetShoppingCartByUserAsync(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            var shoppingCarts = await _shoppingCartRepository.GetShoppingCartByUser(userId);
            return shoppingCarts;
        }

        /// <inheritdoc/>
        public async Task<int> GetShoppingCartCountByUserAsync(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            var count = await _shoppingCartRepository.GetShoppingCartCountByUser(userId);
            return count;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(int cartId)
        {
            ArgumentOutOfRangeException.ThrowIfZero(cartId);

            await _shoppingCartRepository.RemoveAsync(cartId);
            await _shoppingCartRepository.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(ShoppingCartModel cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            _shoppingCartRepository.Update(cart.ToUpdateModel());
            await _shoppingCartRepository.SaveChangesAsync();
        }
    }
}
