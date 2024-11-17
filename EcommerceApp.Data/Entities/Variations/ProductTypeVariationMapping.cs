using System;

namespace EcommerceApp.Data.Entities.Variations;

public class ProductTypeVariationMapping
{
    public int ProductTypeId { get; set; }
    public ProductType ProcuctType { get; set; }
    public int VariationTypeId { get; set; }
    public VariationType VariationType { get; set; }
}
