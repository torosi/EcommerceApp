using System;

namespace EcommerceApp.Data.Entities.Products;

public class Sku : BaseEntity
{
    public string SkuString { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ProductId {get; set;}
    public Product Product { get; set; }
    public ICollection<ProductVariationOption> ProductVariationOptions { get; set; } = new List<ProductVariationOption>();
}
