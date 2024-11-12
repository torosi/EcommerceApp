using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using System.Linq.Expressions;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Method to update product
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product);

        /// <summary>
        /// Method to update multiple products
        /// </summary>
        /// <param name="products"></param>
        public void UpdateRange(IEnumerable<Product> products);

        /// <summary>
        /// Method to get all products
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IEnumerable<Product>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null);

        /// <summary>
        /// Method to get products filtered by expression.
        /// Accepts arguments to allow for pagination
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter">Accepts expression to filter desired products</param>
        /// <param name="pageNumber"></param> // TODO: change these paginaition arguments to be less specific, i.e. change name to offset instead of page number and change items by page to limit
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public Task<(int TotalCount, IEnumerable<Product> Products)> GetFilteredProductsAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null, int pageNumber = 1, int itemsPerPage = 20);
        
        /// <summary>
        /// Method to get product variations
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ProductVariationOption>> GetProductVariationsAsync(int productId);
    }
}
