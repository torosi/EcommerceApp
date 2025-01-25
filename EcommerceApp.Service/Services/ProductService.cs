using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;
using Microsoft.Extensions.Logging;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Variations;
using EcommerceApp.Domain.Extentions.Mappings;

namespace EcommerceApp.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISkuRepository _skuRepository;
        private readonly IProductVariationOptionRepository _productVariationOptionRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ISkuRepository skuRepository, IProductVariationOptionRepository productVariationOptionRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _skuRepository = skuRepository;
            _productVariationOptionRepository = productVariationOptionRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var newProduct = await _productRepository.AddAsync(product.ToCreateModel());
            await _productRepository.SaveChangesAsync();

            _logger.LogDebug("New Product has been added with Id: {id}", product.Id);

            return newProduct;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProductModel>> GetAllAsync(string? includeProperties = null)
        {
            var productEntities = await _productRepository.GetAllAsync(includeProperties);

            _logger.LogDebug("Found '{count}' Product Entities", productEntities.Count());

            return productEntities;
        }

        /// <inheritdoc />
        public async Task<ProductModel?> GetProductById(int productId, string? includeProperties = null)
        {
            if (productId == 0) throw new ArgumentOutOfRangeException(nameof(productId));

            var productEntity = await _productRepository.GetProductById(productId, includeProperties: includeProperties);
            if (productEntity == null) return null;

            _logger.LogDebug("Found Product Entity with id: '{id}'", productEntity.Id);

            return productEntity;
        }

        /// <inheritdoc />
        public async Task RemoveAsync(int productId)
        {
            if (productId == 0) throw new ArgumentOutOfRangeException(nameof(productId));

            _logger.LogDebug("Removing Product Entity with id: '{id}'", productId);
            _productRepository.Remove(productId);

            await _productRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(ProductModel product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            _logger.LogDebug("Found Product Entity with id: '{id}'", product.Id);

            _productRepository.Update(product.ToUpdateModel());
            await _productRepository.SaveChangesAsync();

            _logger.LogDebug("Updated Product Entity with id: '{id}'", product.Id);
        }

        /// <inheritdoc />
        public async Task<(int TotalCount, IEnumerable<ProductModel> Products)> SearchProductByCategoryId(int categoryId, string? includeProperties = null, int pageNumber = 1, int itemsPerPage = 20)
        {
            if (categoryId == 0) throw new ArgumentOutOfRangeException(nameof(categoryId));

            var result = await _productRepository.SearchProductByCategoryId(categoryId, includeProperties, pageNumber, itemsPerPage);

            _logger.LogDebug("'{count}' Product Entities returned from expression", result.TotalCount);

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SkuWithVariationsModel>> GetProductVariationsAsync(int productId)
        {
            var skus = await _productRepository.GetProductVariationsAsync(productId);

            _logger.LogDebug("Found '{count}' Sku Entities", skus.Count());

            return skus;
        }

        /// <inheritdoc />
        public async Task CreateProductVariations(IEnumerable<SkuModel> skus, IEnumerable<ProductVariationOptionInputModel> variations)
        {
            if (skus == null || !skus.Any()) throw new ArgumentNullException(nameof(skus));
            if (variations == null || !variations.Any()) throw new ArgumentNullException(nameof(variations));

            // We only want to save the skus that currently dont exist, so we need to get them from the db and pull out only the ones that are not in out db result
            // TODO : This needs to be revisted, we should only be passing in the new variations, not all of them?

            // Step 1: Retrieve existing SKUs so that we can check that the provided skustrings are unique across all products
            var existingSkus = await _skuRepository.GetBySkuStringsAsync(skus.Select(s => s.SkuString));

            _logger.LogDebug("Found '{count}' existing Sku entities", existingSkus.Count());

            if (existingSkus.Any()) throw new Exception("Sku's provided are not unique");

            // Step 2: Identify SKUs that need to be added
            var newSkus = skus.Where(sku => sku.Id == 0).ToList();
            var savedSkus = new List<SkuModel>(); // These will be the skus back from the db with their new ids

            // Step 3: Save new SKUs
            if (newSkus.Any())
            {
                var createSkuModels = newSkus.Select(s => s.ToCreateModel());
                savedSkus = (await _skuRepository.AddRangeAndSaveAsync(createSkuModels)).ToList();
                await _skuRepository.SaveChangesAsync();

                _logger.LogDebug("'{count}' new Sku entities saved", newSkus.Count());
            }

            if (!savedSkus.Any()) return;

            // Step 4: Combine existing and newly created SKUs
            // var allSkus = existingSkus.Concat(newSkus).ToList(); // add our newly created ones back into our db result so that we have a complete list of them from the db

            // Step 5: get all of the variation options that have an id of 0 (are new)
            // var newVariationOptions = variations.Where(variation => variation.Id == 0).ToList();

            var newVariationOptionModels = variations
                .Where(variation => variation.Id == 0)
                .Select(variation => new ProductVariationOptionModel()
                {
                    SkuId = savedSkus.First(s => s.SkuString == variation.SkuString).Id, // Match by SKU String
                    VariationTypeId = variation.VariationTypeId,
                    VariationValue = variation.VariationValue,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                }).ToList();

            if (newVariationOptionModels.Any())
            {
                // Step 6: Save ProductVariationOption entities in bulk
                await _productVariationOptionRepository.AddRangeAsync(newVariationOptionModels);
                await _productVariationOptionRepository.SaveChangesAsync();

                _logger.LogDebug("'{count}' new ProductVariationOption entities saved", newVariationOptionModels.Count());
            }
        }

    }
}
