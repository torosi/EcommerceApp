using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings
{
    public static class ShoppingCartMappings
    {
        public static ShoppingCartModel ToModel(this ShoppingCart cart)
        {
            return new ShoppingCartModel()
            {
                Id = cart.Id,
                SkuId = cart.SkuId,
                ApplicationUserId = cart.ApplicationUserId,
                Count = cart.Count,
                Product = cart.Sku != null && cart.Sku.Product != null ? cart.Sku.Product.ToModel() : null,
                Sku = cart.Sku != null && cart.Sku.ProductVariationOptions.Any() ? cart.Sku.ToModelWithVariations() : null
            };
        }

        public static ShoppingCart ToEntity(this ShoppingCartModel cart)
        {
            return new ShoppingCart()
            {
                Id = cart.Id,
                SkuId = cart.SkuId,
                ApplicationUserId = cart.ApplicationUserId,
                Count = cart.Count
            };
        }

        public static SkuModel ToModel(this Sku sku)
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

        public static SkuWithVariationsModel ToModelWithVariations(this Sku sku)
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
}
