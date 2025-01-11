using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Test.DomainTests.MappingsTests;

public class CategoryMappingsTests
{
    [Fact]
    public void ToModel_Should_Map_Category_To_CategoryModel_Correctly()
    {
        // Arrange
        var category = new CategoryEntity()
        {
            Id = 1,
            Name = "Test Category",
            Updated = DateTime.Now,
            Created = DateTime.Now,
            Description = "This is a test",
            ImageUrl = "category-image.jpg",
        };

        // Act
        var categoryModel = category.ToDomain();

        // Assert
        Assert.Equal(categoryModel.Id, category.Id);
        Assert.Equal(categoryModel.Name, category.Name);
        Assert.Equal(categoryModel.Updated, category.Updated);
        Assert.Equal(categoryModel.Created, category.Created);
        Assert.Equal(categoryModel.Description, category.Description);
        Assert.Equal(categoryModel.ImageUrl, category.ImageUrl);
    }

    [Fact]
    public void ToEntity_Should_Map_CategoryModel_To_Category_Correctly()
    {
        // Arrange
        var category = new CategoryModel()
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
