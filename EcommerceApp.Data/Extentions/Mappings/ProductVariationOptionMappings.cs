using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings
{
    public static class ProductVariationOptionMappings
    {
        public static ProductVariationOptionEntity ToEntity(this ProductVariationOptionModel option)
        {
            return new ProductVariationOptionEntity()
            {
                Id = option.Id,
                SkuId = option.SkuId,
                VariationTypeId = option.VariationTypeId,
                VariationValue = option.VariationValue
            };
        }

    }
}
