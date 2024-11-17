using System;
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

    public async Task CreateVariationTypeAsync(VariationTypeDto variationTypeDto, int productTypeId)
    {
        if (variationTypeDto == null) throw new ArgumentNullException(nameof(variationTypeDto));
        if (productTypeId == 0) throw new ArgumentNullException(nameof(productTypeId));

        // create new ProductTypeVariationMapping object
        var productTypeVariationMapping = new ProductTypeVariationMapping()
        {
            VariationTypeId = variationTypeDto.Id,
            ProductTypeId = productTypeId   
        };

        // map variationTypeDto to entity
        var variationType = variationTypeDto.ToEntity();

        // save variation type and then the mapping
        await _variationTypeRepository.AddAsync(variationType); // do this one first incase of foreign keys
        await _variationTypeRepository.CreateProductTypeVariationMappingAsync(productTypeVariationMapping);
    }

    public async Task<IEnumerable<VariationTypeDto>> GetAllAsync()
    {
        var variationTypes = await _variationTypeRepository.GetAllAsync();
        var variationDtos = variationTypes.Select(x => x.ToDto());
        return variationDtos;
    }
}
