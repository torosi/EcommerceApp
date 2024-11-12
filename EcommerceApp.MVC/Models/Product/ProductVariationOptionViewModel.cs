namespace EcommerceApp.MVC.Models.Product
{
    public class ProductVariationOptionViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int SkuId { get; set; }
        public string SkuString { get; set; }
        public int Quantity { get; set; }
        public string VariationTypeName { get; set; }
        public string VariationValueName { get; set; }
    }
}
