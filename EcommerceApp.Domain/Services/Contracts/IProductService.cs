using EcommerceApp.Data.Entities;
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
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product?> GetFirstOrDefaultAsync(Expression<Func<Product, bool>> filter);
        public Task AddAsync(Product entity);
        public void Remove(Product entity);
    }
}
