using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Data.Mappings
{
    public static class ShoppingCartMappings
    {
        public static ShoppingCartModel ToDomain(this ShoppingCart cart)
        {
            return new ShoppingCartModel()
            {
                Id = cart.Id,
                SkuId = cart.SkuId,
                ApplicationUserId = cart.ApplicationUserId,
                Count = cart.Count,
                Product = cart.Sku != null && cart.Sku.Product != null ? cart.Sku.Product.ToDomain() : null,
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
        
    }
}
