using EcommerceApp.Domain.Models.Products;
using Microsoft.Extensions.Logging;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Extentions.Mappings;

namespace EcommerceApp.Service.Implementations;

/// <inheritdoc/>
public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ILogger<ProductTypeService> _logger;

    public ProductTypeService(IProductTypeRepository productTypeRepository, ILogger<ProductTypeService> logger)
    {
        _productTypeRepository = productTypeRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(ProductTypeModel productType) // TODO: this should be passing in the create product type model rather than converting it.
    {
        if (productType == null) throw new ArgumentNullException(nameof(productType));

        var newId = await _productTypeRepository.AddAndSaveAsync(productType.ToCreateModel());

        return newId;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductTypeModel>> GetAllAsync()
    {
        var productEntities = await _productTypeRepository.GetAllAsync();
        _logger.LogDebug("Found '{count}' Product Type Entities", productEntities.Count());

        return productEntities;
    }

    /// <inheritdoc />
    public async Task<ProductTypeModel?> GetProductTypeByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id);

        var productType = await _productTypeRepository.GetProductTypeById(id);
        _logger.LogDebug("Found Product Type Entity with id: '{id}'", productType.Id);

        return productType;
    }

    /// <inheritdoc />
    public async Task RemoveAsync(ProductTypeModel productType)
    {
        // this method already deletes the mappings associated with the productType in the db -> must be cascade delete or something???

        if (productType == null) throw new ArgumentNullException(nameof(productType));

        _logger.LogDebug("Removing Product Type Entity with id: '{id}'", productType.Id);

        _productTypeRepository.Remove(productType.Id);
        await _productTypeRepository.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ProductTypeModel productType)
    {
        if (productType == null) throw new ArgumentNullException(nameof(productType));

        _productTypeRepository.Update(productType.ToUpdateModel());
        await _productTypeRepository.SaveChangesAsync();

        _logger.LogDebug("Updated Product Type Entity with id: '{id}'", productType.Id);
    }

}
