using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Domain.Services.Implementations;

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

    public async Task AddAsync(ProductTypeDto productTypeDto)
    {
        if (productTypeDto == null) throw new ArgumentNullException(nameof(productTypeDto));

        var productEntity = productTypeDto.ToEntity();

        await _productTypeRepository.AddAsync(productEntity);
        await _productTypeRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductTypeDto>> GetAllAsync()
    {
        // Get product types
        var productEntities = await _productTypeRepository.GetAllAsync();
        _logger.LogDebug("Found '{count}' Product Type Entities", productEntities.Count());

        // Map and return product types
        return productEntities.Select(x => x.ToDto());
    }

}
