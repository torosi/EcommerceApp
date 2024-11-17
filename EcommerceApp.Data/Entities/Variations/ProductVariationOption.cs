namespace EcommerceApp.Data.Entities.Products;

public class ProductVariationOption : BaseEntity
{
    public int SkuId { get; set; }
    public Sku Sku { get; set; }

    public int VariationTypeId { get; set; }
    public VariationType VariationType { get; set; }

    public int VariationValueId { get; set; }
    public VariationValue VariationValue { get; set; }
}
