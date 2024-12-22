using System;
using EcommerceApp.MVC.Models.ProductVariationOption;

namespace EcommerceApp.MVC.Models.Sku;

public class SkuWithVariationsViewModel
{
    public SkuViewModel Sku { get; set; }
    public ICollection<ProductVariationOptionViewModel> VariationOptions { get; set; } = new List<ProductVariationOptionViewModel>();
    public string VariationOptionsString = string.Empty;
}
