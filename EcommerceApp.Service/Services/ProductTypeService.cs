using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using EcommerceApp.Service.Contracts;

namespace EcommerceApp.Service.Implementations;

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
    public async Task<int> AddAsync(ProductTypeModel entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var productEntity = entity.ToEntity();

        await _productTypeRepository.AddAsync(productEntity);
        await _productTypeRepository.SaveChangesAsync();

        return productEntity.Id;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductTypeModel>> GetAllAsync()
    {
        // Get product types
        var productEntities = await _productTypeRepository.GetAllAsync();
        _logger.LogDebug("Found '{count}' Product Type Entities", productEntities.Count());

        // Map and return product types
        return productEntities.Select(x => x.ToModel());
    }

    /// <inheritdoc />
    public async Task<ProductTypeModel?> GetFirstOrDefaultAsync(Expression<Func<ProductType, bool>> filter, string? includeProperties = null, bool tracked = true)
    {
        // get the product type from db
        var productEntity = await _productTypeRepository.GetFirstOrDefaultAsync(filter, tracked, includeProperties);

        if (productEntity == null)
            return null;

        _logger.LogDebug("Found Product Type Entity with id: '{id}'", productEntity.Id);

        // map and return product type
        return productEntity.ToModel();
    }

    /// <inheritdoc />
    public async Task RemoveAsync(ProductTypeModel entity)
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
    public async Task UpdateAsync(ProductTypeModel entity)
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
