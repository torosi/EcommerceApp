using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Domain.Extentions.Mappings;

public static class CategoryModelMappings
{
    public static UpdateCategoryModel ToUpdateModel(this CategoryModel category)
    {
        return new UpdateCategoryModel()
        {
            Id = category.Id,
            Name = category.Name,
            Updated = category.Updated,
            Created = category.Created,
            Description = category.Description,
            ImageUrl = category.ImageUrl
        };
    }

    public static CreateCategoryModel ToCreateModel(this CategoryModel category)
    {
        return new CreateCategoryModel()
        {
            Id = category.Id,
            Name = category.Name,
            Updated = category.Updated,
            Created = category.Created,
            Description = category.Description,
            ImageUrl = category.ImageUrl
        };
    }
}
