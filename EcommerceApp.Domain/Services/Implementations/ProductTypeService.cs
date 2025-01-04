using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace EcommerceApp.Domain.Services.Implementations;

/// <inheritdoc/>
public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ILogger<ProductTypeService> _logger;
    private readonly IVariationTypeRepository _variationTypeRepository;

    public ProductTypeService(IProductTypeRepository productTypeRepository, ILogger<ProductTypeService> logger, IVariationTypeRepository variationTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
        _logger = logger;
        _variationTypeRepository = variationTypeRepository;
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(ProductTypeDto entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var productEntity = entity.ToEntity();

        await _productTypeRepository.AddAsync(productEntity);
        await _productTypeRepository.SaveChangesAsync();

        return productEntity.Id;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductTypeDto>> GetAllAsync()
    {
        // Get product types
        var productEntities = await _productTypeRepository.GetAllAsync();
        _logger.LogDebug("Found '{count}' Product Type Entities", productEntities.Count());

        // Map and return product types
        return productEntities.Select(x => x.ToDto());
    }

    /// <inheritdoc />
    public async Task<ProductTypeDto?> GetFirstOrDefaultAsync(Expression<Func<ProductType, bool>> filter, string? includeProperties = null, bool tracked = true)
    {
        // get the product type from db
        var productEntity = await _productTypeRepository.GetFirstOrDefaultAsync(filter, tracked, includeProperties);

        if (productEntity == null) 
            return null;

        _logger.LogDebug("Found Product Type Entity with id: '{id}'", productEntity.Id);

        // map and return product type
        return productEntity.ToDto();
    }

    /// <inheritdoc />
    public async Task RemoveAsync(ProductTypeDto entity)
    {
        // this method already deletes the mappings associated with the productType in the db -> must be cascade delete or something???

        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var productEntity = await _productTypeRepository.GetFirstOrDefaultAsync(x => x.Id == entity.Id);

        if (productEntity != null)
        {
            _logger.LogDebug("Removing Product Type Entity with id: '{id}'", entity.Id);
            _productTypeRepository.Remove(productEntity);
            await _productTypeRepository.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ProductTypeDto entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var productFromDb = await _productTypeRepository.GetFirstOrDefaultAsync(x => x.Id == entity.Id);

        _logger.LogDebug("Found Product Type Entity with id: '{id}'", entity.Id);

        if (productFromDb != null)
        {
            productFromDb.Name = entity.Name;
            productFromDb.Description = entity.Description;
            productFromDb.Updated = DateTime.Now;

            _productTypeRepository.Update(productFromDb);
            await _productTypeRepository.SaveChangesAsync();

            _logger.LogDebug("Updated Product Type Entity with id: '{id}'", entity.Id);
        }
    }

}
