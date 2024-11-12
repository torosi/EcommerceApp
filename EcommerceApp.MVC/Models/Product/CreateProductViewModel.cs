using EcommerceApp.MVC.Models.Category;

namespace EcommerceApp.MVC.Models.Product
{
    public class CreateProductViewModel
    {
        public ProductViewModel Product { get; set; }

        // currently it makes a request to get these values so we are not passing them as viewmodels at the moment.
        // i might leave it as is just to demonstrate another way to do it.
        //public IEnumerable<CategoryViewModel> Categories { get; set; }
        //public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; }


        // do we create one option when you set up the product and then let you add variations to it later?
        // or do we create it so you can add them all now? a list of options or just one option? i think list would be better but interface would be harder
        //public IEnumerable<ProductVariationOptionViewModel> Options { get; set; }
    }
}
