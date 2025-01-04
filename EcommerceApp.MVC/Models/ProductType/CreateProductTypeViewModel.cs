using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.VariationType;

namespace EcommerceApp.MVC.Models.ProductType
{
    public class CreateProductTypeViewModel
    {
        public ProductTypeViewModel ProductType  { get; set; }
        public IList<CreateVariationTypeViewModel> VariationTypes { get; set; }
    }
}
