using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Test.MappingsTests;

public class ProductVariationOptionMappingsTests
{
    [Fact]
    public void ToEntity_Should_Map_ProductVariationOptionModel_To_ProductVariationOptionEntity_Correctly()
    {
        // Arrange
        var productVariationOptionModel = new ProductVariationOptionModel()
        {
            Id = 1,
            SkuId = 2,
            VariationTypeId = 3,
            VariationTypeName = "testvariation",
            VariationValue = "testvalue",
            Created = DateTime.Now,
            Updated = DateTime.Now
        };

        // Act
        var result = productVariationOptionModel.ToEntity();
        
        // Assert
        Assert.Equal(productVariationOptionModel.Id, result.Id);
        Assert.Equal(productVariationOptionModel.SkuId, result.SkuId);
        Assert.Equal(productVariationOptionModel.VariationTypeId, result.VariationTypeId);
        Assert.Equal(productVariationOptionModel.VariationValue, result.VariationValue);
    }

    
}
