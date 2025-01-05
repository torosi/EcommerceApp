using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Dtos.Variations;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;
using System.Linq.Expressions;

namespace EcommerceApp.Domain.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISkuRepository _skuRepository;
        private readonly IProductVariationOptionRepository _productVariationOptionRepository;

        public ProductService(IProductRepository productRepository, ISkuRepository skuRepository, IProductVariationOptionRepository productVariationOptionRepository)
        {
            _productRepository = productRepository;
            _skuRepository = skuRepository;
            _productVariationOptionRepository = productVariationOptionRepository;
        }

        /// <inheritdoc />
        public async Task<ProductDto> AddAsync(ProductDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var productEntity = entity.ToEntity();

            await _productRepository.AddAsync(productEntity);
            await _productRepository.SaveChangesAsync();

            return productEntity.ToDto();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProductDto>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null)
        {
            var productEntities = await _productRepository.GetAllAsync(includeProperties, filter);
            return productEntities.Select(x => x.ToDto());
        }

        /// <inheritdoc />
        public async Task<ProductDto?> GetFirstOrDefaultAsync(Expression<Func<Product, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            var productEntity = await _productRepository.GetFirstOrDefaultAsync(filter, includeProperties: includeProperties, tracked: tracked);
            
            if (productEntity == null) 
                return null;

            return productEntity.ToDto(); 
        }

        /// <inheritdoc />
        public async Task RemoveAsync(ProductDto entity)
        {
            if (entity == null) throw new ArgumentException(nameof(entity));
            _productRepository.Remove(entity.ToEntity());
            await _productRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(ProductDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // 1) get the entity from db
            var productFromDb = await _productRepository.GetFirstOrDefaultAsync(x => x.Id == entity.Id);

            // 2) update entity
            if (productFromDb != null)
            {
                productFromDb.Description = entity.Description;
                productFromDb.Name = entity.Name;
                productFromDb.Updated = DateTime.Now;
                productFromDb.ImageUrl = entity.ImageUrl;
                productFromDb.CategoryId = entity.CategoryId;
                productFromDb.ProductTypeId = entity.ProductTypeId;

                _productRepository.Update(productFromDb);
                await _productRepository.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task<(int TotalCount, IEnumerable<ProductDto> Products)> GetFilteredProductsAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null, int pageNumber = 1, int itemsPerPage = 20)
        {
            var productResult = await _productRepository.GetFilteredProductsAsync(includeProperties, filter, pageNumber, itemsPerPage);
            var productDtos = productResult.Products.Select(x => x.ToDto());
            return (productResult.TotalCount, productDtos);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SkuWithVariationsDto>> GetProductVariationsAsync(int productId)
        {
            var skus = await _productRepository.GetProductVariationsAsync(productId);

            // TODO: This can be made into an extention method in mappings?
            var skuDtos = skus.Select(sku => new SkuWithVariationsDto
            {
                SkuId = sku.Id,
                SkuString = sku.SkuString,
                Quantity = sku.Quantity,
                ProductId = sku.ProductId,
                VariationOptions = sku.ProductVariationOptions.Select(option => new ProductVariationOptionDto
                {
                    Id = option.Id,
                    SkuId = sku.Id,
                    VariationTypeId = option.VariationTypeId,
                    VariationValue = option.VariationValue,
                    VariationTypeName = option.VariationType.Name
                }).ToList()
            }).ToList();

            return skuDtos;
        }

        /// <inheritdoc />
        public async Task CreateProductVariations(IEnumerable<SkuDto> skus, IEnumerable<ProductVariationOptionInputDto> variations)
        {
            if (skus == null || !skus.Any()) throw new ArgumentNullException(nameof(skus));
            if (variations == null || !variations.Any()) throw new ArgumentNullException(nameof(variations));

            // We only want to save the skus that currently dont exist, so we need to get them from the db and pull out only the ones that are not in out db result

            // Step 1: Retrieve existing SKUs so that we can check that the provided skustrings are unique across all products
            var existingSkus = await _skuRepository.GetBySkuStringsAsync(skus.Select(s => s.SkuString));

            if (existingSkus != null) throw new Exception("Sku's provided are not unique");

            // Step 2: Identify SKUs that need to be added
            var newSkus = skus
                .Where(sku => sku.Id == 0)
                .Select(x => new Sku()
                {
                    SkuString = x.SkuString,
                    Quantity = x.Quantity,
                    ProductId = x.ProductId,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                }).ToList();

            // Step 3: Save new SKUs
            if (newSkus.Any())
            {
                await _skuRepository.AddRangeAsync(newSkus);
                await _skuRepository.SaveChangesAsync();
            }

            // // Step 4: Combine existing and newly created SKUs
            // var allSkus = existingSkus.Concat(newSkus).ToList(); // add our newly created ones back into our db result so that we have a complete list of them from the db

            // Step 5: get all of the variation options that have an id of 0 (are new)
            var newVariationOptionEntities = variations
                .Where(variation => variation.Id == 0)
                .Select(variation => new ProductVariationOption()
                {
                    SkuId = newSkus.First(s => s.SkuString == variation.SkuString).Id, // Match by SKU String
                    VariationTypeId = variation.VariationTypeId,
                    VariationValue = variation.VariationValue,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                }).ToList();

            if (newVariationOptionEntities.Any())
            {
                // Step 6: Save ProductVariationOption entities in bulk
                await _productVariationOptionRepository.AddRangeAsync(newVariationOptionEntities);
                await _productVariationOptionRepository.SaveChangesAsync(); 
            }
        }

    }
}
