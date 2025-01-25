using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;

namespace EcommerceApp.Domain.Extentions.Mappings
{
    public static class ProductVariationOptionModelMappings
    {
        public static CreateProductVariationOptionModel ToCreateModel(this ProductVariationOptionModel option)
        {
            return new CreateProductVariationOptionModel()
            {
                Id = option.Id,
                SkuId = option.SkuId,
                VariationTypeId = option.VariationTypeId,
                VariationTypeName = option.VariationTypeName,
                VariationValue = option.VariationValue,
                Created = option.Created,
                Updated = option.Updated
            };
        }
    }
}
