using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models;

namespace EcommerceApp.Data.Mappings
{
    public static class ProductMappings
    {

        public static ProductModel ToDomain(this ProductEntity product)
        {
            return new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Category = product.Category != null ? product.Category.ToDomain() : null,
                ProductTypeId = product.ProductTypeId,
                ProductType = product.ProductType != null ? product.ProductType.ToDomain() : null,
                Price = product.Price,
            };
        }

        public static ProductEntity ToEntity(this ProductModel product)
        {
            return new ProductEntity()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ProductTypeId = product.ProductTypeId
            };
        }

    }
}
