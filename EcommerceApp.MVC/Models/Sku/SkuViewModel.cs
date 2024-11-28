namespace EcommerceApp.MVC.Models.Sku
{
    public class SkuViewModel
    {
        public int Id { get; set; }        
        public string SkuString { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
