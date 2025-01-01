using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context, ILogger<ShoppingCartRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        /// <inheritdoc />
        public void Update(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId)
        {
            IQueryable<ShoppingCart> query = _dbSet
                .Where(x => x.ApplicationUserId == userId)
                .Include(x => x.Sku)
                .ThenInclude(s => s.Product)
                .Include(x => x.Sku)
                .ThenInclude(s => s.ProductVariationOptions)
                .ThenInclude(v => v.VariationType);

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<int> GetShoppingCartCountByUser(string userId)
        {
            IQueryable<ShoppingCart> query = _dbSet;

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
    }
}
