using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings
{
    public static class ProductVariationOptionMappings
    {
        public static ProductVariationOption ToEntity(this ProductVariationOptionModel option)
        {
            return new ProductVariationOption()
            {
                Id = option.Id,
                SkuId = option.SkuId,
                VariationTypeId = option.VariationTypeId,
                VariationValue = option.VariationValue
            };
        }

    }
}
