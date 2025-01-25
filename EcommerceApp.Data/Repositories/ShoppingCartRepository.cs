using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context, ILogger<ShoppingCartRepository> logger)
        {
            _context = context;
        }

        /// <inheritdoc />
        public void Update(UpdateShoppingCartModel cart)
        {
            _context.ShoppingCarts.Update(cart.ToEntity());
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ShoppingCartModel>> GetShoppingCartByUser(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            IQueryable<ShoppingCartEntity> query = _context.Set<ShoppingCartEntity>()
                .Where(x => x.ApplicationUserId == userId)
                .Include(x => x.Sku)
                .ThenInclude(s => s.Product)
                .Include(x => x.Sku)
                .ThenInclude(s => s.ProductVariationOptions)
                .ThenInclude(v => v.VariationType);

            var shoppingCarts = await query.ToListAsync();

            return shoppingCarts.Select(x => x.ToDomain());
        }

        /// <inheritdoc />
        public async Task<int> GetShoppingCartCountByUser(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            IQueryable<ShoppingCartEntity> query = _context.Set<ShoppingCartEntity>();

            query = query.Where(x => x.ApplicationUserId == userId);

            if (await query.AnyAsync())
            {
                return await query.SumAsync(x => x.Count);
            }
            else
            {
                return 0;
            }
        }

        public async Task AddAsync(CreateShoppingCartModel cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var cartEntity = cart.ToEntity();
            cartEntity.Created = DateTime.Now;
            cartEntity.Updated = DateTime.Now;

            await _context.ShoppingCarts.AddAsync(cartEntity);

        }

        public async Task RemoveAsync(int cartId)
        {
            ArgumentOutOfRangeException.ThrowIfZero(cartId);

            var cartEntity = await _context.ShoppingCarts.SingleOrDefaultAsync(x => x.Id == cartId);

            if (cartEntity != null)
            {
                _context.ShoppingCarts.Remove(cartEntity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(ShoppingCartModel cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var cartEntity = cart.ToEntity();
            cartEntity.Updated = DateTime.Now;

            _context.ShoppingCarts.Update(cartEntity);
        }

        public async Task<ShoppingCartModel?> GetShoppingCartByIdAsync(int cartId)
        {
            ArgumentOutOfRangeException.ThrowIfZero(cartId);

            var shoppingCartEntity = await _context.ShoppingCarts
                .Include(c => c.Sku)
                .ThenInclude(s => s.Product)
                .SingleOrDefaultAsync(x => x.Id == cartId);

            return shoppingCartEntity == null ? null : shoppingCartEntity.ToDomain();
        }
    }
}
