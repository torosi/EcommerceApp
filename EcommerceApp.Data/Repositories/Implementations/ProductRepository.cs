using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Data.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EcommerceApp.Data.Entities.Products;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null)
        {
            IQueryable<Product> query = _dbSet;

            // Apply filtering logic
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void UpdateRange(IEnumerable<Product> products)
        {
            if (products == null || !products.Any()) throw new ArgumentNullException(nameof(products));

            _context.Products.UpdateRange(products);
        }

        /// <summary>
        /// A method that will apply a filter to get products as well as pagination options.
        /// Returns the total of number of products also so that you can calculate number of pages if needed.
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns>Returns (totalNumberOfProducts, IEnumerable of your products)</returns>
        public async Task<(int TotalCount, IEnumerable<Product> Products)> GetFilteredProductsAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null, int pageNumber = 1, int itemsPerPage = 20)
        {
            IQueryable<Product> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            int totalCount = await query.CountAsync();

            List<Product> products = await query
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            return (totalCount, products);
        }

        public async Task<IEnumerable<ProductVariationOption>> GetProductVariationsAsync(int productId)
        {

            var options = await _context.ProductVariationOptions
                .Where(o => o.Sku.ProductId == productId)
                .Include(o => o.Sku)
                .Include(o => o.Sku.Product)
                .Include(o => o.VariationType)
                .Include(o => o.VariationValue)
                .ToListAsync();

            return options;
        }
    }
}
