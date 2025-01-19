using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Mappings;

namespace EcommerceApp.Test.MappingsTests;

public class SkuMappingsTests
{
    [Fact]
    public void ToDomain_Should_Map_SkuEntity_To_SkuModel_Correctly()
    {
        // Arrange
        var skuEntity = new SkuEntity()
        {
            Id = 1,
            Updated = DateTime.Now,
            Created = DateTime.Now,
            SkuString = "testproduct-1",
            Quantity = 10,
            ProductId = 1
        };

        // Act
        var skuModel = skuEntity.ToDomain();

        // Assert
        Assert.Equal(skuModel.Id, skuEntity.Id);
        Assert.Equal(skuModel.Created, skuEntity.Created);
        Assert.Equal(skuModel.Updated, skuEntity.Updated);
        Assert.Equal(skuModel.SkuString, skuEntity.SkuString);
        Assert.Equal(skuModel.ProductId, skuEntity.ProductId);
        Assert.Equal(skuModel.Quantity, skuEntity.Quantity);
    }
}
