namespace EcommerceApp.MVC.Models.ProductVariationOption
{
    public class ProductVariationOptionViewModel
    {
        public int VariationTypeId { get; set; }
        public string? VariationTypeName { get; set; } = string.Empty;
        public string VariationValue { get; set; } = string.Empty;
    }
}
