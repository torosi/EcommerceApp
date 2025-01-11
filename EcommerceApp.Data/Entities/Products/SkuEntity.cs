namespace EcommerceApp.Data.Entities.Products;

public class SkuEntity : BaseEntity
{
    public string SkuString { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ProductId {get; set;}
    public ProductEntity Product { get; set; }
    public ICollection<ProductVariationOptionEntity> ProductVariationOptions { get; set; } = new List<ProductVariationOptionEntity>();
}
