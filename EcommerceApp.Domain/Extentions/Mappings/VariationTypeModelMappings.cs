using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;

namespace EcommerceApp.Domain.Extentions.Mappings
{
    public static class VariationTypeModelMappings
    {
        public static CreateVariationTypeModel ToCreateModel(this VariationTypeModel variationType)
        {
            return new CreateVariationTypeModel()
            {
                Id = variationType.Id,
                Created = variationType.Created,
                Updated = variationType.Updated,
                Name = variationType.Name,
            };
        }
    }
}
