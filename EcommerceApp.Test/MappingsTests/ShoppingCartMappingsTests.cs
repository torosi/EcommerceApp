using System;
using System.Xml.Serialization;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Mappings;

namespace EcommerceApp.Test.MappingsTests;

public class ShoppingCartMappingsTests
{
    [Fact]
    public void ToModel_Should_Map_ShoppingCart_To_ShoppingCartModel_Correctly()
    {
        // Arrange
        var shoppingCart = new ShoppingCart()
        {
            Id = 1,
            SkuId = 19,
            ApplicationUserId = "testuserid",
            Count = 10,
        };

        // Act
        var shoppingCartModel = shoppingCart.ToModel();

        // Assert
        Assert.Equal(shoppingCartModel.Id, shoppingCart.Id);
        Assert.Equal(shoppingCartModel.SkuId, shoppingCart.SkuId);
        Assert.Equal(shoppingCartModel.ApplicationUserId, shoppingCart.ApplicationUserId);
        Assert.Equal(shoppingCartModel.Count, shoppingCart.Count);
    }

    [Fact]
    public void ToEntity_Should_Map_ShoppingCartModel_To_ShoppingCart_Correctly()
    {
        // Arrange
        var shoppingCartModel = new ShoppingCartModel()
        {
            Id = 2,
            SkuId = 10,
            ApplicationUserId = "applicationUserId",
            Count = 100,
        };

        // Act
        var shoppingCart = shoppingCartModel.ToEntity();

        // Assert
        Assert.Equal(shoppingCartModel.Id, shoppingCart.Id);
        Assert.Equal(shoppingCartModel.SkuId, shoppingCart.SkuId);
        Assert.Equal(shoppingCartModel.ApplicationUserId, shoppingCart.ApplicationUserId);
        Assert.Equal(shoppingCartModel.Count, shoppingCart.Count);
    }

    [Fact]
    public void ToModel_Should_Map_Sku_To_SkuModel_Correctly()
    {
        // Arrange
        var sku = new Sku()
        {
            Id = 3,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            SkuString = "testskustring",
            Quantity = 7,
            ProductId = 1
        };

        // Act
        var skuModel = sku.ToModel();

        // Assert
        Assert.Equal(skuModel.Id, sku.Id);
        Assert.Equal(skuModel.Created, sku.Created);
        Assert.Equal(skuModel.Updated, sku.Updated);
        Assert.Equal(skuModel.SkuString, sku.SkuString);
        Assert.Equal(skuModel.Quantity, sku.Quantity);
        Assert.Equal(skuModel.ProductId, sku.ProductId);
    }

    [Fact]
    public void ToModelWithVariations_Should_Map_Sku_To_SkuWithVariationsModel_Correctly()
    {
        // Arrange
        var sku = new Sku()
        {
            Id = 3,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            SkuString = "testskustring",
            Quantity = 7,
            ProductId = 1,
            ProductVariationOptions = new List<ProductVariationOption>()
            {
                new ProductVariationOption()
                {
                    Id = 3,
                    SkuId = 3,
                    VariationTypeId = 1,
                    VariationValue = "testvalue1",
                    VariationType = new VariationType()
                    {
                        Id = 2, 
                        Name = "testvaraitiontype1"
                    }
                },
                new ProductVariationOption()
                {
                    Id = 4,
                    SkuId = 3,
                    VariationTypeId = 2,
                    VariationValue = "testvalue2",
                    VariationType = new VariationType()
                    {
                        Id = 3,
                        Name = "testvaraitiontype2"
                    }
                }
            }
        };

        // Act
        var skuWithVariationModel = sku.ToModelWithVariations();

        // Assert
        Assert.Equal(sku.Id, sku.Id);
        Assert.Equal(sku.Created, sku.Created);
        Assert.Equal(sku.Updated, sku.Updated);
        Assert.Equal(sku.SkuString, sku.SkuString);
        Assert.Equal(sku.Quantity, sku.Quantity);
        Assert.Equal(sku.ProductId, sku.ProductId);

        Assert.NotNull(skuWithVariationModel.VariationOptions);
        Assert.Equal(sku.ProductVariationOptions.Count, skuWithVariationModel.VariationOptions.Count);

        for (int i = 0; i < sku.ProductVariationOptions.Count(); i++)
        {
            var originalOptions = sku.ProductVariationOptions.OrderBy(x => x.Id).ToList();
            var expectedOptions = skuWithVariationModel.VariationOptions.OrderBy(x => x.Id).ToList();

            var expectedOption = expectedOptions[i];
            var originalOption = originalOptions[i];

            Assert.Equal(originalOption.Id, expectedOption.Id);
            Assert.Equal(originalOption.SkuId, expectedOption.SkuId);
            Assert.Equal(originalOption.VariationTypeId, expectedOption.VariationTypeId);
            Assert.Equal(originalOption.VariationValue, expectedOption.VariationValue);
            Assert.Equal(originalOption.VariationType.Name, expectedOption.VariationTypeName);
        }

    }
}
