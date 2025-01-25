using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Sku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Extentions.Mappings
{
    public static class SkuModelMappings
    {
        public static CreateSkuModel ToCreateModel(this SkuModel sku)
        {
            return new CreateSkuModel()
            {
                Id = sku.Id,
                Created = sku.Created,
                Updated = sku.Updated,
                ProductId = sku.ProductId,
                Quantity = sku.Quantity,
                SkuString = sku.SkuString
            };
        }
    }
}
