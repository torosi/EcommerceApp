using System;

namespace EcommerceApp.MVC.Models.ProductVariationOption;

public class VariationValueInputViewModel
{
    public int Id { get; set; }
    public int VariationTypeId { get; set; }
    public string Value { get; set; }
}