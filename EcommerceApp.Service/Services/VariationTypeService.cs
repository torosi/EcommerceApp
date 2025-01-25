using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Variations;
using EcommerceApp.Domain.Extentions.Mappings;

namespace EcommerceApp.Service.Implementations;

public class VariationTypeService : IVariationTypeService
{
    private readonly IVariationTypeRepository _variationTypeRepository;

    public VariationTypeService(IVariationTypeRepository variationTypeRepository)
    {
        _variationTypeRepository = variationTypeRepository;
    }

    /// <inheritdoc />
    public async Task CreateVariationTypeAsync(VariationTypeModel variationTypeModel)
    {
        if (variationTypeModel == null) throw new ArgumentNullException(nameof(variationTypeModel));
        await _variationTypeRepository.AddAsync(variationTypeModel.ToCreateModel());
        await _variationTypeRepository.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeModel>> GetAllAsync(string? includeProperties = null)
    {
        var variationTypes = await _variationTypeRepository.GetAllAsync(includeProperties: includeProperties);
        return variationTypes;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeModel>> GetAllByProductTypeAsync(int productTypeId)
    {
        if (productTypeId == 0) throw new ArgumentNullException(nameof(productTypeId));

        var variationTypes = await _variationTypeRepository.GetAllByProductTypeAsync(productTypeId);

        // If no variation types are found, return an empty collection
        if (variationTypes == null || !variationTypes.Any())
            return Enumerable.Empty<VariationTypeModel>();

        // Map entities to DTOs
        var variationTypeModels = variationTypes.Select(vt => new VariationTypeModel
        {
            Id = vt.Id,
            Name = vt.Name,
            Created = vt.Created,
            Updated = vt.Updated
        });

        return variationTypeModels;
    }

    /// <inheritdoc />
    public async Task CreateProductTypeAndVariationTypeMappings(IEnumerable<int> variationTypeIds, int productTypeId)
    {
        if (!variationTypeIds.Any() || variationTypeIds.Contains(0)) throw new ArgumentNullException(nameof(variationTypeIds));
        if (productTypeId == 0) throw new ArgumentNullException(nameof(productTypeId));

        // create ProductVaraitionTypeMapping
        var productTypeVariationMappings = new List<ProductTypeVariationMappingModel>();

        foreach (var id in variationTypeIds)
        {
            productTypeVariationMappings.Add(new ProductTypeVariationMappingModel()
            {
                VariationTypeId = id,
                ProductTypeId = productTypeId
            });
        }

        // save new ProductVaraitionTypeMappings
        await _variationTypeRepository.CreateProductTypeVariationMappingRangeAsync(productTypeVariationMappings);
        await _variationTypeRepository.SaveChangesAsync();
    }
}
