using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Services.Contracts
{
    public interface IShoppingCartService
    {
        public Task AddAsync(ShoppingCartDto cart);

        /// <summary>
        /// Method to get the first of default shopping cart by expression
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tracked"></param>
        /// <returns></returns>
        public Task<ShoppingCartDto> GetFirstOrDefaultAsync(Expression<Func<ShoppingCart, bool>> filter, bool tracked = true);
       
        /// <summary>
        /// Method to update a shopping cart
        /// </summary>
        /// <param name="cart"></param>
        public Task UpdateAsync(ShoppingCartDto cart);

        /// <summary>
        /// Method to get all shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ShoppingCartDto>> GetShoppingCartByUserAsync(string userId);

        /// <summary>
        /// Method to remove a shopping cart by Id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public Task<bool> RemoveAsync(int cartId);

        /// <summary>
        /// Method to get the total quantity of shopping cart items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<int> GetShoppingCartCountByUserAsync(string userId);
    }
}
