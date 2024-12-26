using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EcommerceApp.Domain.Mappings
{
    public static class ShoppingCartMappings
    {
        public static ShoppingCartDto ToDto(this ShoppingCart cart)
        {
            return new ShoppingCartDto()
            {
                Id = cart.Id,
                SkuId = cart.SkuId,
                ApplicationUserId = cart.ApplicationUserId,
                Count = cart.Count,
                Product = cart.Sku != null && cart.Sku.Product != null ? cart.Sku.Product.ToDto() : null,
                Sku = cart.Sku != null ? cart.Sku.ToDto() : null
            };
        }

        public static ShoppingCart ToEntity(this ShoppingCartDto cart)
        {
            return new ShoppingCart()
            {
                Id = cart.Id,
                SkuId = cart.SkuId,
                ApplicationUserId = cart.ApplicationUserId,
                Count = cart.Count
            };
        }

        public static SkuDto ToDto(this Sku sku)
        {
            return new SkuDto()
            {
                Id = sku.Id,
                Created = sku.Created,
                Updated = sku.Updated,
                SkuString = sku.SkuString,
                Quantity = sku.Quantity,
                ProductId = sku.ProductId
            };
        }
        
    }
}
