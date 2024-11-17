using System;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Mappings;

public static class VariationTypeMappings
{
    public static VariationTypeDto ToDto(this VariationType variationType)
    {
        return new VariationTypeDto() 
        {
            Id = variationType.Id,
            Name = variationType.Name,
            Created = variationType.Created,
            Updated = variationType.Updated,
        };
    }

    public static VariationType ToEntity(this VariationTypeDto variationTypeDto)
    {
        return new VariationType() 
        {
            Id = variationTypeDto.Id,
            Name = variationTypeDto.Name,
            Created = variationTypeDto.Created,
            Updated = variationTypeDto.Updated,
        };
    }
}
