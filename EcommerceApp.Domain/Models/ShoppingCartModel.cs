using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int SkuId { get; set; }
        public SkuWithVariationsModel? Sku { get; set; }
        public int Count { get; set; }
        public ProductModel? Product { get; set;}
    }
}
