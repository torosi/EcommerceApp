using System;
using EcommerceApp.MVC.Models.Sku;

namespace EcommerceApp.MVC.Models.ProductVariationOption;

public class ProductVariationOptionInputViewModel
{
    public SkuViewModel Sku { get; set; }
    public List<VariationValueInputViewModel> VariationValues { get; set; } = new List<VariationValueInputViewModel>();
}
