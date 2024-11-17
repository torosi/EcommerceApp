using System;

namespace EcommerceApp.Data.Entities.Products;

public class ProductTypeVariationMapping
{
    public int ProductTypeId { get; set; }
    public ProductType? ProcuctType { get; set; }
    public int VariationTypeId { get; set; }
    public VariationType? VariationType { get; set; }
}
