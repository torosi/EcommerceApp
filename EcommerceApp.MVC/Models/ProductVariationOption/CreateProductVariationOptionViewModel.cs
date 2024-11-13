namespace EcommerceApp.MVC.Models.ProductVariationOption
{
    public class CreateProductVariationOptionViewModel
    {
        public string SkuString { get; set; }
        public int Quantity { get; set; }
        public int VariationTypeId { get; set; }
        public int VariationValueId { get; set; }
    }
}
