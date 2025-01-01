using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Implementations;
using Moq;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Dtos.Products;
using System.Linq.Expressions;

namespace EcommerceApp.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ISkuRepository> _skuRepositoryMock;
    private readonly Mock<IProductVariationOptionRepository> _productVariationOptionRepositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _skuRepositoryMock = new Mock<ISkuRepository>();
        _productVariationOptionRepositoryMock = new Mock<IProductVariationOptionRepository>();

        _productService = new ProductService(
            _productRepositoryMock.Object,
            _skuRepositoryMock.Object,
            _productVariationOptionRepositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Product_And_Return_Dto()
    {
        // Arrange
        var productDto = new ProductDto {
            Id = 0,
            Name = "Test Product",
            Updated = DateTime.Now,
            Created = DateTime.Now,
            Description = "Test Description",
            ImageUrl = "testImageUrl",
            ProductTypeId = 1,
            Price = 11.99
        };

        var productEntity = productDto.ToEntity();

        _productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        _productRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _productService.AddAsync(productDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.Name, result.Name);

        _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once); // how many times is has been called
        _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task UpdateAsync_Should_Update_Existing_Product()
    {
        // Arrange
        var productDto = new ProductDto
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            ImageUrl = "updated-image.jpg",
            CategoryId = 2,
            ProductTypeId = 3
        };

        var productEntity = new Product
        {
            Id = 1,
            Name = "Old Product",
            Description = "Old Description",
            ImageUrl = "old-image.jpg",
            CategoryId = 1,
            ProductTypeId = 1
        };

        _productRepositoryMock.Setup(r => r.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), false, null))
                              .ReturnsAsync(productEntity);
        _productRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _productService.UpdateAsync(productDto);

        // Assert
        Assert.Equal(productDto.Name, productEntity.Name);
        Assert.Equal(productDto.Description, productEntity.Description);
        Assert.Equal(productDto.ImageUrl, productEntity.ImageUrl);
        Assert.Equal(productDto.CategoryId, productEntity.CategoryId);
        Assert.Equal(productDto.ProductTypeId, productEntity.ProductTypeId);

        _productRepositoryMock.Verify(r => r.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), false, null), Times.Once);
        _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
        _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}