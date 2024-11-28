using EcommerceApp.MVC.Models.Product;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EcommerceApp.MVC.Models.ProductVariationOption
{
    public class CreateProductVariationOptionViewModel
    {
        public int ProductId { get; set; }

        // this will be out input view model to create new product variations from
        public List<ProductVariationOptionInputViewModel> Input { get; set; } = new List<ProductVariationOptionInputViewModel>();

        // this is to be passed into the view so that we can display all of the variation types
        [ValidateNever]
        public List<VariationTypeViewModel> VariationTypes { get; set; } = new List<VariationTypeViewModel>();
    }
}
