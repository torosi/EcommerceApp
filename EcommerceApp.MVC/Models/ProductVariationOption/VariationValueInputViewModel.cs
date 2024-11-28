using System;

namespace EcommerceApp.MVC.Models.ProductVariationOption;

public class VariationValueInputViewModel
{
    public int VariationTypeId { get; set; }
    public string Value { get; set; } // Could be parsed into an integer if needed
}