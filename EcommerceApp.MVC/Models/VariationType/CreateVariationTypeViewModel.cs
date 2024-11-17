using System;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductType;

namespace EcommerceApp.MVC.Models.VariationType;

public class CreateVariationTypeViewModel
{
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; }
}
