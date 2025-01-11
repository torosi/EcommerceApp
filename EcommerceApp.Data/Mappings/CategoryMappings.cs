using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Data.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryEntity ToEntity(this CategoryModel model)
        {
            return new CategoryEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Updated = model.Updated,
                Created = model.Created,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };
        }

        public static CategoryEntity ToEntity(this UpdateCategoryModel model)
        {
            return new CategoryEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Updated = model.Updated,
                Created = model.Created,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };
        }

        public static CategoryEntity ToEntity(this CreateCategoryModel model)
        {
            return new CategoryEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Updated = model.Updated,
                Created = model.Created,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };
        }

        public static CategoryModel ToDomain(this CategoryEntity entity)
        {
            return new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Updated = entity.Updated,
                Created = entity.Created,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
            };
        }


    }
}