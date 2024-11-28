using System;

namespace EcommerceApp.Domain.Dtos.Products;

public class SkuWithVariationsDto
{
    // TODO - change these properties to just a skuDto object
    public int SkuId { get; set; }
    public string SkuString { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public ICollection<ProductVariationOptionDto> VariationOptions { get; set; } = new List<ProductVariationOptionDto>();
}
