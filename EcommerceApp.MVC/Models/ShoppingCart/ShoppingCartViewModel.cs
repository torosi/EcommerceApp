using EcommerceApp.MVC.Models.Product;

namespace EcommerceApp.MVC.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
