using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Extentions.Mappings;

public static class ProductModelMappings
{
    public static UpdateProductModel ToUpdateModel(this ProductModel product)
    {
        return new UpdateProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Updated = product.Updated,
            Created = product.Created,
            ImageUrl = product.ImageUrl,
            ProductTypeId = product.ProductTypeId,
            Price = product.Price
        };
    }

    public static CreateProductModel ToCreateModel(this ProductModel product)
    {
        return new CreateProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Updated = product.Updated,
            Created = product.Created,
            ImageUrl = product.ImageUrl,
            ProductTypeId = product.ProductTypeId,
            Price = product.Price
        };
    }
}
