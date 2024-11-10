using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product product);
        public void UpdateRange(IEnumerable<Product> products);
        public Task<IEnumerable<Product>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null);
        public Task<(int TotalCount, IEnumerable<Product> Products)> GetFilteredProductsAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null, int pageNumber = 1, int itemsPerPage = 20);
        public Task<IEnumerable<ProductVariationOption>> GetProductVariationsAsync(int productId);
    }
}
