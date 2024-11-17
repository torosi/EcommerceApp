using System;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos.Products;
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

    public async Task<IEnumerable<VariationTypeDto>> GetAllAsync()
    {
        var variationTypes = await _variationTypeRepository.GetAllAsync();
        var variationDtos = variationTypes.Select(x => x.ToDto());
        return variationDtos;
    }
}
