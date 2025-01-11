namespace EcommerceApp.Data.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public virtual CategoryEntity? Category { get; set; } // navigation property

        public int ProductTypeId { get; set; }
        public virtual ProductTypeEntity ProductType { get; set; }

    }
}