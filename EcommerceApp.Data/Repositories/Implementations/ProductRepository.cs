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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        /// <inheritdoc />
        public void UpdateRange(IEnumerable<Product> products)
        {
            if (products == null || !products.Any()) throw new ArgumentNullException(nameof(products));

            _context.Products.UpdateRange(products);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<IEnumerable<Sku>> GetProductVariationsAsync(int productId)
        {
            return await _context.Skus
                .Where(sku => sku.ProductId == productId)
                .Include(sku => sku.ProductVariationOptions)
                .ThenInclude(option => option.VariationType)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateProductVariations(List<ProductVariationOption> variations)
        {
            await _context.ProductVariationOptions.AddRangeAsync(variations);
        }

    }
}
