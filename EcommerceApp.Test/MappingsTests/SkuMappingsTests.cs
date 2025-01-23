using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Models.Products;

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

    [Fact]
    public void ToEntity_Should_Map_SkuModel_To_SkuEntity_Correctly()
    {
        // Arrange
        var skuModel = new SkuModel()
        {
            Id = 2,
            Updated = DateTime.Now,
            Created = DateTime.Now,
            SkuString = "testproduct-2",
            Quantity = 100,
            ProductId = 4
        };

        // Act
        var skuEntity = skuModel.ToEntity();

        // Assert
        Assert.Equal(skuModel.Id, skuEntity.Id);
        Assert.Equal(skuModel.Created, skuEntity.Created);
        Assert.Equal(skuModel.Updated, skuEntity.Updated);
        Assert.Equal(skuModel.SkuString, skuEntity.SkuString);
        Assert.Equal(skuModel.ProductId, skuEntity.ProductId);
        Assert.Equal(skuModel.Quantity, skuEntity.Quantity);
    }

    [Fact]
    public void ToDomainWithVariations_Should_Map_SkuEntity_To_SkuModel_Correctly()
    {
        // Arrange
        var skuEntity = new SkuEntity()
        {
            Id = 1,
            Updated = DateTime.Now,
            Created = DateTime.Now,
            SkuString = "testproduct-1",
            Quantity = 10,
            ProductId = 1,
            ProductVariationOptions = new List<ProductVariationOptionEntity>()
            {
                new ProductVariationOptionEntity()
                {
                    Id = 1,
                    SkuId = 1, 
                    VariationTypeId = 3,
                    VariationValue = "testvalue1",
                    VariationType = new VariationTypeEntity()
                    {
                        Id = 3,
                        Name = "test variation type 1",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }
                },
                new ProductVariationOptionEntity()
                {
                    Id = 2,
                    SkuId = 1,
                    VariationTypeId = 2,
                    VariationValue = "testvalue2",
                    VariationType = new VariationTypeEntity()
                    {
                        Id = 2,
                        Name = "test variation type 2",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }
                }
            }
        };

        // Act
        var skuModel = skuEntity.ToDomainWithVariations();

        // Assert
        Assert.Equal(skuModel.SkuId, skuEntity.Id);
        Assert.Equal(skuModel.SkuString, skuEntity.SkuString);
        Assert.Equal(skuModel.ProductId, skuEntity.ProductId);
        Assert.Equal(skuModel.Quantity, skuEntity.Quantity);

        var entityOptions = skuEntity.ProductVariationOptions.OrderBy(x => x.Id).ToList(); // to ensure that they are in the same order
        var modelOptions = skuModel.VariationOptions.OrderBy(x => x.Id).ToList();

        // loop through the original list and compare it to the new list of variations
        for (var i = 0; i < entityOptions.Count(); i++)
        {
            var currentEntityOption = entityOptions[i];
            var currentModelOption = modelOptions[i];

            Assert.Equal(currentEntityOption.Id, currentModelOption.Id);
            Assert.Equal(currentEntityOption.SkuId, currentModelOption.SkuId);
            Assert.Equal(currentEntityOption.VariationValue, currentModelOption.VariationValue);
            Assert.Equal(currentEntityOption.VariationTypeId, currentModelOption.VariationTypeId);
            Assert.Equal(currentEntityOption.VariationType.Name, currentModelOption.VariationTypeName);
        }
    }
}
