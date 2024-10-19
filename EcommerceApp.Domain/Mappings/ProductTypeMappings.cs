using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Mappings;

public static class ProductTypeMappings
{
    public static ProductTypeDto ToDto(this ProductType productType)
    {
        return new ProductTypeDto()
        {
            Id = productType.Id,
            Name = productType.Name,
            Created = productType.Created,
            Updated = productType.Updated,
            Description = productType.Description
        };
    }

    public static ProductType ToEntity(this ProductTypeDto productTypeDto)
    {
        return new ProductType()
        {
            Id = productTypeDto.Id,
            Name = productTypeDto.Name,
            Created = productTypeDto.Created,
            Updated = productTypeDto.Updated,
            Description = productTypeDto.Description
        };
    }
}
