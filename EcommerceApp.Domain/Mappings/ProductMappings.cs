using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;

namespace EcommerceApp.Domain.Mappings
{
    public static class ProductMappings
    {

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Category = product.Category != null ? product.Category.ToDto() : null,
                ProductTypeId = product.ProductTypeId,
                ProductType = product.ProductType != null ? product.ProductType.ToDto() : null,
                Price = product.Price,
            };
        }

        public static Product ToEntity(this ProductDto product)
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
