using System;

namespace EcommerceApp.Data.Entities.Products;

public class ProductTypeVariationMappingEntity
{
    public int ProductTypeId { get; set; }
    public ProductTypeEntity? ProcuctType { get; set; }
    public int VariationTypeId { get; set; }
    public VariationTypeEntity? VariationType { get; set; }
}
