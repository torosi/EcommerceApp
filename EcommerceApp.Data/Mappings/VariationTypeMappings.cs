using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings;

public static class VariationTypeMappings
{
    public static VariationTypeModel ToDomain(this VariationTypeEntity variationType)
    {
        return new VariationTypeModel() 
        {
            Id = variationType.Id,
            Name = variationType.Name,
            Created = variationType.Created,
            Updated = variationType.Updated,
        };
    }

    public static VariationTypeEntity ToEntity(this VariationTypeModel variationTypeModel)
    {
        return new VariationTypeEntity() 
        {
            Id = variationTypeModel.Id,
            Name = variationTypeModel.Name,
            Created = variationTypeModel.Created,
            Updated = variationTypeModel.Updated,
        };
    }
}
