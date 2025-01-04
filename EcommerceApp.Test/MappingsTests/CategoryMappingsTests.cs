using System;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Mappings;

namespace EcommerceApp.Test.DomainTests.MappingsTests;

public class CategoryMappingsTests
{
    [Fact]
    public void ToDto_Should_Map_Category_To_CategoryDto_Correctly()
    {
        // Arrange
        var category = new Category()
        {
            Id = 1,
            Name = "Test Category",
            Updated = DateTime.Now,
            Created = DateTime.Now,
            Description = "This is a test",
            ImageUrl = "category-image.jpg",
        };

        // Act
        var categoryDto = category.ToDto();

        // Assert
        Assert.Equal(categoryDto.Id, category.Id);
        Assert.Equal(categoryDto.Name, category.Name);
        Assert.Equal(categoryDto.Updated, category.Updated);
        Assert.Equal(categoryDto.Created, category.Created);
        Assert.Equal(categoryDto.Description, category.Description);
        Assert.Equal(categoryDto.ImageUrl, category.ImageUrl);
    }

    [Fact]
    public void ToEntity_Should_Map_CategoryDto_To_Category_Correctly()
    {
        // Arrange
        var category = new CategoryDto()
        {
            Id = 1,
            Name = "Test Category",
            Updated = DateTime.Now,
            Created = DateTime.Now,
            Description = "This is a test",
            ImageUrl = "category-image.jpg",
        };

        // Act
        var categoryEntity = category.ToEntity();

        // Assert
        Assert.Equal(categoryEntity.Id, category.Id);
        Assert.Equal(categoryEntity.Name, category.Name);
        Assert.Equal(categoryEntity.Updated, category.Updated);
        Assert.Equal(categoryEntity.Created, category.Created);
        Assert.Equal(categoryEntity.Description, category.Description);
        Assert.Equal(categoryEntity.ImageUrl, category.ImageUrl);
    }

}
