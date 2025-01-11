using EcommerceApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Data.Mappings;

namespace EcommerceApp.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProductModel>> GetAllAsync(string? includeProperties = null)
        {
            IQueryable<Product> query = _context.Set<Product>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            var productEntities = await query.ToListAsync();
            return productEntities.Select(x => x.ToDomain());
        }

        /// <inheritdoc />
        public void Update(ProductModel product)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            var productEntity = product.ToEntity();
            _context.Products.Update(productEntity);
        }

        /// <inheritdoc />
        public void UpdateRange(IEnumerable<ProductModel> products)
        {
            if (products == null || !products.Any()) throw new ArgumentNullException(nameof(products));
            var productEntities = products.Select(x => x.ToEntity());
            _context.Products.UpdateRange(productEntities);
        }

        /// <inheritdoc />
        public async Task<(int TotalCount, IEnumerable<ProductModel> Products)> SearchProductByCategoryId(int categoryId, string? includeProperties = null, int pageNumber = 1, int itemsPerPage = 20)
        {
            if (categoryId == 0) throw new ArgumentNullException(nameof(categoryId));

            IQueryable<Product> query = _context.Set<Product>();

            query = query.Where(x => x.CategoryId == categoryId);

            int totalCount = await query.CountAsync();

            List<Product> productEntities = await query
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            var productModels = productEntities.Select(x => x.ToDomain());

            return (totalCount, productModels);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SkuWithVariationsModel>> GetProductVariationsAsync(int productId)
        {
            var skusWithVariations = await _context.Skus
                .Where(sku => sku.ProductId == productId && sku.ProductVariationOptions.Any()) // to be like an inner join as there might be a sku without options (ideally shouldnt be but for now this will make sure the page loads)
                .Include(sku => sku.ProductVariationOptions)
                .ThenInclude(option => option.VariationType)
                .ToListAsync();

            return skusWithVariations.Select(x => x.ToModelWithVariations());
        }

        /// <inheritdoc />
        public async Task CreateProductVariations(List<ProductVariationOptionModel> variations)
        {
            if (variations == null) throw new ArgumentNullException(nameof(variations));
            var variationEntities = variations.Select(x => x.ToEntity());
            await _context.ProductVariationOptions.AddRangeAsync(variationEntities);
        }

        /// <inheritdoc />
        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));

            var productEntity = product.ToEntity();
            await _context.Products.AddAsync(productEntity);

            return productEntity.ToDomain();
        }

        /// <inheritdoc />
        public async void Remove(int id)
        {
            if (id == 0) throw new ArgumentOutOfRangeException(nameof(id));

            var productFromDb = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (productFromDb != null)
            {
                _context.Products.Remove(productFromDb);
            }
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<ProductModel?> GetProductById(int productId = 0, bool tracked = true, string? includeProperties = null)
        {
            if (productId == 0) throw new ArgumentOutOfRangeException(nameof(productId));

            var productEntity = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
            return productEntity != null ? productEntity.ToDomain() : null;
        }
    }
}
