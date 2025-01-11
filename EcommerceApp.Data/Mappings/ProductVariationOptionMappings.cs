using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
