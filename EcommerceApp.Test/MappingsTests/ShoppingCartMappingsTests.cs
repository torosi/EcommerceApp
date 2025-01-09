using System;
using System.Xml.Serialization;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;

namespace EcommerceApp.Test.MappingsTests;

public class ShoppingCartMappingsTests
{
    [Fact]
    public void ToDto_Should_Map_ShoppingCart_To_ShoppingCartDto_Correctly()
    {
        var shoppingCart = new ShoppingCart()
        {
            Id = 1,
            SkuId = 19,
            ApplicationUserId = "testuserid",
            Count = 10,
        };

        var shoppingCartDto = shoppingCart.ToDto();

        Assert.Equal(shoppingCartDto.Id, shoppingCart.Id);
        Assert.Equal(shoppingCartDto.SkuId, shoppingCart.SkuId);
        Assert.Equal(shoppingCartDto.ApplicationUserId, shoppingCart.ApplicationUserId);
        Assert.Equal(shoppingCartDto.Count, shoppingCart.Count);
    }

    [Fact]
    public void ToEntity_Should_Map_ShoppingCartDto_To_ShoppingCart_Correctly()
    {
        var shoppingCartDto = new ShoppingCartDto()
        {
            Id = 2,
            SkuId = 10,
            ApplicationUserId = "applicationUserId",
            Count = 100,
        };

        var shoppingCart = shoppingCartDto.ToEntity();

        Assert.Equal(shoppingCartDto.Id, shoppingCart.Id);
        Assert.Equal(shoppingCartDto.SkuId, shoppingCart.SkuId);
        Assert.Equal(shoppingCartDto.ApplicationUserId, shoppingCart.ApplicationUserId);
        Assert.Equal(shoppingCartDto.Count, shoppingCart.Count);
    }

    [Fact]
    public void ToDto_Should_Map_Sku_To_SkuDto_Correctly()
    {
        var sku = new Sku()
        {
            Id = 3,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            SkuString = "testskustring",
            Quantity = 7,
            ProductId = 1
        };

        var skuDto = sku.ToDto();

        Assert.Equal(skuDto.Id, sku.Id);
        Assert.Equal(skuDto.Created, sku.Created);
        Assert.Equal(skuDto.Updated, sku.Updated);
        Assert.Equal(skuDto.SkuString, sku.SkuString);
        Assert.Equal(skuDto.Quantity, sku.Quantity);
        Assert.Equal(skuDto.ProductId, sku.ProductId);
    }

}
