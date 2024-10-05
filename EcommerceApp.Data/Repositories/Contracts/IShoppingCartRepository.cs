using EcommerceApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public void Update(ShoppingCart cart);
    }
}
