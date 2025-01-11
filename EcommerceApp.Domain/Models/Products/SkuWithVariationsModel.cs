namespace EcommerceApp.Domain.Models.Products;

public class SkuWithVariationsModel
{
    // TODO - change these properties to just a skuModel object
    public int SkuId { get; set; }
    public string SkuString { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public ICollection<ProductVariationOptionModel> VariationOptions { get; set; } = new List<ProductVariationOptionModel>();
}
