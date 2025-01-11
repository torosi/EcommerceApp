namespace EcommerceApp.Domain.Models.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public UpdateCategoryModel ToUpdateModel()
        {
            return new UpdateCategoryModel
            {
                Id = this.Id,
                Name = this.Name,
                Updated = this.Updated,
                Created = this.Created,
                Description = this.Description,
                ImageUrl = this.ImageUrl,
            };
        }

        public CreateCategoryModel ToCreateModel()
        {
            return new CreateCategoryModel
            {
                Id = this.Id,
                Name = this.Name,
                Updated = this.Updated,
                Created = this.Created,
                Description = this.Description,
                ImageUrl = this.ImageUrl,
            };
        }
    }
}