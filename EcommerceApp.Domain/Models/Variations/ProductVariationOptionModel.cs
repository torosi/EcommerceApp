namespace EcommerceApp.Domain.Models.Products
{
    public class ProductVariationOptionModel
    {
        public int Id { get; set; }
        public int SkuId { get; set; }
        public int VariationTypeId { get; set; }
        public string VariationTypeName { get; set; }= string.Empty;
        public string VariationValue { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
