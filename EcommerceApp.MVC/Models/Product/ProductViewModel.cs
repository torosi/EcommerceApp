using EcommerceApp.MVC.Models.Category;

namespace EcommerceApp.MVC.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; }
        public double Price { get; set; }
    }
}
