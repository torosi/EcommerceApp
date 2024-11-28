using System.ComponentModel.DataAnnotations;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.MVC.Models.ProductType;
using EcommerceApp.MVC.Models.ProductVariationOption;

namespace EcommerceApp.MVC.Models.Product
{
    public class ProductViewModel // TODO: Change this to product details view model as it has the extra stuff on it.
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string? ImageUrl { get; set; }
        public double Price { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductTypeId { get; set; }
    }
}
