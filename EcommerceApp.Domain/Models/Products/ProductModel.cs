using EcommerceApp.Domain.Models.Category;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public CategoryModel? Category { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeModel? ProductType { get; set; }
        public double Price { get; set; }

    }
}
