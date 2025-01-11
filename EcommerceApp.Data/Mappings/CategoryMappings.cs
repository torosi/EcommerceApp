using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Data.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryModel ToModel(this Category category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Updated = category.Updated,
                Created = category.Created,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
            };
        }

        public static Category ToEntity(this CategoryModel categoryModel)
        {
            return new Category()
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Updated = categoryModel.Updated,
                Created = categoryModel.Created,
                Description = categoryModel.Description,
                ImageUrl = categoryModel.ImageUrl,
            };
        }

    }
}