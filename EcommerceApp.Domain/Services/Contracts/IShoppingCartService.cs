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
        public Task<ShoppingCartDto> GetFirstOrDefault(Expression<Func<ShoppingCart, bool>> filter, bool tracked = true);
        public Task Update(ShoppingCartDto cart);
    }
}
