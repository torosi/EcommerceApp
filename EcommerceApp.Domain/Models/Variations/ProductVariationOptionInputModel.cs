namespace EcommerceApp.Domain.Models.Variations;

public class ProductVariationOptionInputModel
{
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SkuString { get; set; } = string.Empty;
        public int VariationTypeId { get; set; }
        public string VariationValue { get; set; } = string.Empty;
}
