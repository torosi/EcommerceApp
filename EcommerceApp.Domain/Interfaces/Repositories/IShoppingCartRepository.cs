using EcommerceApp.Domain.Models.ShoppingCart;

namespace EcommerceApp.Domain.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        /// <summary>
        /// Method to update a shopping cart
        /// </summary>
        /// <param name="cart"></param>
        void Update(UpdateShoppingCartModel cart);

        /// <summary>
        /// Method to get all shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<ShoppingCartModel>> GetShoppingCartByUser(string userId);

        /// <summary>
        /// Method to get the total quantity of shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetShoppingCartCountByUser(string userId);
        Task AddAsync(CreateShoppingCartModel model);
        Task SaveChangesAsync();
        Task RemoveAsync(int cartId);
        Task<ShoppingCartModel?> GetShoppingCartByIdAsync(int cartId);

    }
}
