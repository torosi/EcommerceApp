using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
