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

        public async Task AddAsync(ShoppingCartDto cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var shoppingCartEntity = cart.ToEntity();
            shoppingCartEntity.Created = DateTime.Now;
            shoppingCartEntity.Updated = DateTime.Now;
            await _shoppingCartRepository.AddAsync(shoppingCartEntity);
            await _shoppingCartRepository.SaveChangesAsync();
        }

        public async Task<ShoppingCartDto?> GetFirstOrDefault(Expression<Func<ShoppingCart, bool>> filter, bool tracked = true)
        {
            var shoppingCart = await _shoppingCartRepository.GetFirstOrDefaultAsync(filter, tracked: tracked);
            if (shoppingCart == null) return null;
            return shoppingCart.ToDto();
        }

        public async Task Update(ShoppingCartDto cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var shoppingCart = cart.ToEntity();
            shoppingCart.Updated = DateTime.Now;
            _shoppingCartRepository.Update(shoppingCart);
            await _shoppingCartRepository.SaveChangesAsync();
        }
    }
}
