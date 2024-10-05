using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId)
        {
            IQueryable<ShoppingCart> query = _dbSet;

            query = query.Where(x => x.ApplicationUserId == userId);
            query = query.Include("Product");

            return await query.ToListAsync();
        }
    }
}
