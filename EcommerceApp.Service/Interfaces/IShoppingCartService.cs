using EcommerceApp.Domain.Models.ShoppingCart;
using System.Linq.Expressions;

namespace EcommerceApp.Service.Contracts
{
    public interface IShoppingCartService
    {
        public Task AddAsync(ShoppingCartModel cart);

        /// <summary>
        /// Method to get the first of default shopping cart by expression
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tracked"></param>
        /// <returns></returns>
        Task<ShoppingCartModel?> GetShoppingCartById(int cartId);

        /// <summary>
        /// Method to update a shopping cart
        /// </summary>
        /// <param name="cart"></param>
        Task UpdateAsync(ShoppingCartModel cart);

        /// <summary>
        /// Method to get all shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<ShoppingCartModel>> GetShoppingCartByUserAsync(string userId);

        /// <summary>
        /// Method to remove a shopping cart by Id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int cartId);

        /// <summary>
        /// Method to get the total quantity of shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetShoppingCartCountByUserAsync(string userId);
    }
}
