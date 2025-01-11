using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models;

namespace EcommerceApp.Data.Mappings
{
    public static class ProductMappings
    {

        public static ProductModel ToModel(this Product product)
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
                Category = product.Category != null ? product.Category.ToModel() : null,
                ProductTypeId = product.ProductTypeId,
                ProductType = product.ProductType != null ? product.ProductType.ToModel() : null,
                Price = product.Price,
            };
        }

        public static Product ToEntity(this ProductModel product)
        {
            return new Product()
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
