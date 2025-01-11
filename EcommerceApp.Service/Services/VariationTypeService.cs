using System.Linq.Expressions;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.Service.Contracts;

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
        // if (productTypeId == 0) throw new ArgumentNullException(nameof(productTypeId));

        // map variationTypeModel to entity
        var variationType = variationTypeModel.ToEntity();

        // save variation type and then the mapping
        await _variationTypeRepository.AddAsync(variationType); // do this one first incase of foreign keys
        await _variationTypeRepository.SaveChangesAsync(); // we have to save the product type first so that we have the id to add the mappings table

    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeModel>> GetAllAsync(string? includeProperties = null, Expression<Func<VariationType, bool>>? filter = null)
    {
        var variationTypes = await _variationTypeRepository.GetAllAsync(includeProperties: includeProperties, filter: filter);
        var variationModels = variationTypes.Select(x => x.ToModel());
        return variationModels;
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
        var productTypeVariationMappings = new List<ProductTypeVariationMapping>();

        foreach (var id in variationTypeIds)
        {
            productTypeVariationMappings.Add(new ProductTypeVariationMapping()
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
