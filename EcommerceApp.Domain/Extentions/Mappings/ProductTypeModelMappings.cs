using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.ProductType;

namespace EcommerceApp.Domain.Extentions.Mappings
{
    public static class ProductTypeModelMappings
    {
        public static UpdateProductTypeModel ToUpdateModel(this ProductTypeModel productType)
        {
            return new UpdateProductTypeModel()
            {
                Id = productType.Id,
                Name = productType.Name, 
                Created = productType.Created,
                Updated = productType.Updated,
                Description = productType.Description
            };
        }

        public static CreateProductTypeModel ToCreateModel(this ProductTypeModel productType)
        {
            return new CreateProductTypeModel()
            {
                Id = productType.Id,
                Name = productType.Name,
                Created = productType.Created,
                Updated = productType.Updated,
                Description = productType.Description
            };
        }
    }
}
