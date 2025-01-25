using EcommerceApp.Domain.Models.ShoppingCart;

namespace EcommerceApp.Domain.Extentions.Mappings
{
    public static class ShoppingCartModelMappings
    {
        public static CreateShoppingCartModel ToCreateModel(this ShoppingCartModel cart)
        {
            return new CreateShoppingCartModel()
            {
                Id = cart.Id,
                ApplicationUserId = cart.ApplicationUserId,
                SkuId = cart.SkuId,
                Count = cart.Count
            };
        }

        public static UpdateShoppingCartModel ToUpdateModel(this ShoppingCartModel cart)
        {
            return new UpdateShoppingCartModel()
            {
                Id = cart.Id,
                ApplicationUserId = cart.ApplicationUserId,
                SkuId = cart.SkuId,
                Count = cart.Count
            };
        }
    }
}
