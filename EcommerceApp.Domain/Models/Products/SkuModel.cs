namespace EcommerceApp.Domain.Models.Products
{
    public class SkuModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SkuString { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
