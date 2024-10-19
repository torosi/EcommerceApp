namespace EcommerceApp.Data.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public override string? ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}";
        }
    }
}
