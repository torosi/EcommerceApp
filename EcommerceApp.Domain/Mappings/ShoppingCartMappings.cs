using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;

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
                Product = cart.Sku.Product.ToDto()
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
    }
}
