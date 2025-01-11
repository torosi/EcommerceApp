using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Mappings;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities;
using Microsoft.Extensions.Logging;
using EcommerceApp.Service.Contracts;

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
        public async Task AddAsync(ShoppingCartModel cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var shoppingCartEntity = cart.ToEntity();
            shoppingCartEntity.Created = DateTime.Now;
            shoppingCartEntity.Updated = DateTime.Now;
            await _shoppingCartRepository.AddAsync(shoppingCartEntity);
            await _shoppingCartRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ShoppingCartModel?> GetFirstOrDefaultAsync(Expression<Func<ShoppingCart, bool>> filter, bool tracked = true)
        {
            var shoppingCart = await _shoppingCartRepository.GetFirstOrDefaultAsync(filter, tracked: tracked, includeProperties: "Sku");
            if (shoppingCart == null) return null;
            return shoppingCart.ToModel();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ShoppingCartModel>> GetShoppingCartByUserAsync(string userId)
        {
            var shoppingCartEntities = await _shoppingCartRepository.GetShoppingCartByUser(userId);
            var shoppingCartModels = shoppingCartEntities.Select(x => x.ToModel()).ToList();
            return shoppingCartModels;
        }

        /// <inheritdoc/>
        public async Task<int> GetShoppingCartCountByUserAsync(string userId)
        {
            var count = await _shoppingCartRepository.GetShoppingCartCountByUser(userId);
            return count;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(int cartId)
        {
            var shoppingCartEntity = await _shoppingCartRepository.GetFirstOrDefaultAsync(x => x.Id == cartId);

            var success = false;

            if (shoppingCartEntity != null)
            {
                _shoppingCartRepository.Remove(shoppingCartEntity);
                await _shoppingCartRepository.SaveChangesAsync();
                success = true;
            }

            return success;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(ShoppingCartModel cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var shoppingCart = cart.ToEntity();
            shoppingCart.Updated = DateTime.Now;
            _shoppingCartRepository.Update(shoppingCart);
            await _shoppingCartRepository.SaveChangesAsync();
        }
    }
}
