using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings;

public static class ProductTypeMappings
{
    public static ProductTypeModel ToDomain(this ProductTypeEntity productType)
    {
        return new ProductTypeModel()
        {
            Id = productType.Id,
            Name = productType.Name,
            Created = productType.Created,
            Updated = productType.Updated,
            Description = productType.Description
        };
    }

    public static ProductTypeEntity ToEntity(this ProductTypeModel productTypeModel)
    {
        return new ProductTypeEntity()
        {
            Id = productTypeModel.Id,
            Name = productTypeModel.Name,
            Created = productTypeModel.Created,
            Updated = productTypeModel.Updated,
            Description = productTypeModel.Description
        };
    }
}
