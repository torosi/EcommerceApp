using System.Text.Json.Serialization;
using Microsoft.Identity.Client;

namespace EcommerceApp.Data.Entities.Products;

public class ProductVariationOption : BaseEntity
{
    public int Id { get; set; }
    public int SkuId { get; set; }
    public Sku Sku { get; set; }

    public int VariationTypeId { get; set; }
    public VariationType VariationType { get; set; }

    // public int VariationValueId { get; set; }
    // public VariationValue VariationValue { get; set; }
    public string VariationValue { get; set; } = string.Empty;
}
