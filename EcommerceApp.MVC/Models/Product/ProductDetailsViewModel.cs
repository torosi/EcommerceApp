using System;
using EcommerceApp.MVC.Models.Sku;

namespace EcommerceApp.MVC.Models.Product;

public class ProductDetailsViewModel
{
    public ProductViewModel Product { get; set; } = new ProductViewModel();

    // Grouped variations for dropdowns
    public Dictionary<string, List<string>> GroupedVariations { get; set; } = new Dictionary<string, List<string>>();
    public IEnumerable<SkuWithVariationsViewModel> SkusWithVariations { get; set; } = new List<SkuWithVariationsViewModel>();
}
