using System;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductType;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EcommerceApp.MVC.Models.VariationType;

public class CreateVariationTypeViewModel
{
    public string Name { get; set; }
    public int ProductTypeId { get; set; }

    [ValidateNever]
    public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; }
}
