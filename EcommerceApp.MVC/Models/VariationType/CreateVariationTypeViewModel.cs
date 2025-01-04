using System;
using System.Diagnostics.Eventing.Reader;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductType;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EcommerceApp.MVC.Models.VariationType;

public class CreateVariationTypeViewModel
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; } = false;
}
