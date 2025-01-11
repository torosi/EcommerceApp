using EcommerceApp.Domain.Models;

namespace EcommerceApp.Domain.Interfaces.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartModel>
    {
        /// <summary>
        /// Method to update a shopping cart
        /// </summary>
        /// <param name="cart"></param>
        public void Update(ShoppingCartModel cart);

        /// <summary>
        /// Method to get all shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ShoppingCartModel>> GetShoppingCartByUser(string userId);

        /// <summary>
        /// Method to get the total quantity of shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<int> GetShoppingCartCountByUser(string userId);
    }
}
