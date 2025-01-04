using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Dtos.Variations;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;

namespace EcommerceApp.Domain.Services.Implementations;

public class VariationTypeService : IVariationTypeService
{
    private readonly IVariationTypeRepository _variationTypeRepository;

    public VariationTypeService(IVariationTypeRepository variationTypeRepository)
    {
        _variationTypeRepository = variationTypeRepository;
    }

    /// <inheritdoc />
    public async Task CreateVariationTypeAsync(VariationTypeDto variationTypeDto)
    {
        if (variationTypeDto == null) throw new ArgumentNullException(nameof(variationTypeDto));
        // if (productTypeId == 0) throw new ArgumentNullException(nameof(productTypeId));

        // map variationTypeDto to entity
        var variationType = variationTypeDto.ToEntity();

        // save variation type and then the mapping
        await _variationTypeRepository.AddAsync(variationType); // do this one first incase of foreign keys
        await _variationTypeRepository.SaveChangesAsync(); // we have to save the product type first so that we have the id to add the mappings table

        // create new ProductTypeVariationMapping object
        // var productTypeVariationMapping = new ProductTypeVariationMapping()
        // {
        //     VariationTypeId = variationType.Id,
        //     ProductTypeId = productTypeId   
        // };

        // await _variationTypeRepository.CreateProductTypeVariationMappingAsync(productTypeVariationMapping);
        // await _variationTypeRepository.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeDto>> GetAllAsync(string? includeProperties = null, Expression<Func<VariationType, bool>>? filter = null)
    {
        var variationTypes = await _variationTypeRepository.GetAllAsync(includeProperties: includeProperties, filter: filter);
        var variationDtos = variationTypes.Select(x => x.ToDto());
        return variationDtos;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeDto>> GetAllByProductTypeAsync(int productTypeId)
    {
        if (productTypeId == 0) throw new ArgumentNullException(nameof(productTypeId));

        var variationTypes = await _variationTypeRepository.GetAllByProductTypeAsync(productTypeId);
        
        // If no variation types are found, return an empty collection
        if (variationTypes == null || !variationTypes.Any())
            return Enumerable.Empty<VariationTypeDto>();

        // Map entities to DTOs
        var variationTypeDtos = variationTypes.Select(vt => new VariationTypeDto
        {
            Id = vt.Id,
            Name = vt.Name,
            Created = vt.Created,
            Updated = vt.Updated
        });

        return variationTypeDtos;
    }
}
