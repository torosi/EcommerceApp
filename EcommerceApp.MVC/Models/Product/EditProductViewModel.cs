using EcommerceApp.MVC.Models.Category;

namespace EcommerceApp.MVC.Models.Product
{
    public class EditProductViewModel
    {
        public ProductViewModel Product { get; set; }
        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; }
        public IEnumerable<CategoryViewModel> Categories {get; set; }
    }   
}
