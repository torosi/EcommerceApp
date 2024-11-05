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
        /// <summary>
        /// Method to update a shopping cart
        /// </summary>
        /// <param name="cart"></param>
        public void Update(ShoppingCart cart);

        /// <summary>
        /// Method to get all shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId);

        /// <summary>
        /// Method to get the total quantity of shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<int> GetShoppingCartCountByUser(string userId);
    }
}
