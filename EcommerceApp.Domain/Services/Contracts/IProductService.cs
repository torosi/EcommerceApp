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
    public interface IProductService
    {
        /// <summary>
        /// A method to retrieve all product.
        /// </summary>
        /// <returns>A collection of products.</returns>
        public Task<IEnumerable<ProductDto>> GetAllAsync();
        public Task<ProductDto?> GetFirstOrDefaultAsync(Expression<Func<Product, bool>> filter);
        public Task AddAsync(ProductDto entity);
        public Task RemoveAsync(ProductDto entity);
    }
}
