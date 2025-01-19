using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings;

public static class SkuMappings
{
    public static SkuModel ToDomain(this SkuEntity sku)
    {
        return new SkuModel()
        {
            Id = sku.Id,
            Created = sku.Created,
            Updated = sku.Updated,
            SkuString = sku.SkuString,
            Quantity = sku.Quantity,
            ProductId = sku.ProductId,
        };
    }

    public static SkuEntity ToEntity(this SkuModel model)
    {
        return new SkuEntity()
        {
            Id = model.Id,
            SkuString = model.SkuString,
            Quantity = model.Quantity,
            ProductId = model.ProductId,
            Updated = model.Updated,
            Created = model.Created,
        };
    }

    public static SkuWithVariationsModel ToModelWithVariations(this SkuEntity sku)
    {
        return new SkuWithVariationsModel
        {
            SkuId = sku.Id,
            SkuString = sku.SkuString,
            Quantity = sku.Quantity,
            ProductId = sku.ProductId,
            VariationOptions = sku.ProductVariationOptions.Select(option => new ProductVariationOptionModel
            {
                Id = option.Id,
                SkuId = sku.Id,
                VariationTypeId = option.VariationTypeId,
                VariationValue = option.VariationValue,
                VariationTypeName = option.VariationType.Name
            }).ToList()
        };
    }

}
