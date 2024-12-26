using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Data.Repositories.Implementations;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Domain.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }


        /// <inheritdoc/>
        public async Task AddAsync(ShoppingCartDto cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var shoppingCartEntity = cart.ToEntity();
            shoppingCartEntity.Created = DateTime.Now;
            shoppingCartEntity.Updated = DateTime.Now;
            await _shoppingCartRepository.AddAsync(shoppingCartEntity);
            await _shoppingCartRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ShoppingCartDto?> GetFirstOrDefaultAsync(Expression<Func<ShoppingCart, bool>> filter, bool tracked = true)
        {
            var shoppingCart = await _shoppingCartRepository.GetFirstOrDefaultAsync(filter, tracked: tracked, includeProperties: "Sku");
            if (shoppingCart == null) return null;
            return shoppingCart.ToDto();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ShoppingCartDto>> GetShoppingCartByUserAsync(string userId)
        {
            var shoppingCartEntities = await _shoppingCartRepository.GetShoppingCartByUser(userId);
            var shoppingCartDtos = shoppingCartEntities.Select(x => x.ToDto()).ToList();
            return shoppingCartDtos;
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
        public async Task UpdateAsync(ShoppingCartDto cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var shoppingCart = cart.ToEntity();
            shoppingCart.Updated = DateTime.Now;
            _shoppingCartRepository.Update(shoppingCart);
            await _shoppingCartRepository.SaveChangesAsync();
        }
    }
}
