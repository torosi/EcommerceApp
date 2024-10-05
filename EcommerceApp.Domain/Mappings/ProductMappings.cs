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
                Price = product.Price,
                //Variations = product.Variations != null ? product.Variations.Select(x => x.ToDto()).ToList() : null
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
                //Variations = product.Variations != null ? product.Variations.Select(x => x.ToEntity()).ToList() : null
            };
        }

        public static ProductVariation ToEntity(this ProductVariationDto product)
        {
            return new ProductVariation()
            {
                ProductId = product.ProductId,
                Size = product.Size,
                Stock = product.Stock,
                Colour = product.Colour,
                Price = product.Price,
                Product = product.Product.ToEntity()
            };
        }

        public static ProductVariationDto ToDto(this ProductVariation product)
        {
            return new ProductVariationDto()
            {
                ProductId = product.ProductId,
                Size = product.Size,
                Stock = product.Stock,
                Colour = product.Colour,
                Price = product.Price,
                Product = product.Product.ToDto()
            };
        }


    }
}
