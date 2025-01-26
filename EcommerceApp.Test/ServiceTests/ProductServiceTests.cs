using Moq;
using Microsoft.Extensions.Logging;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Service.Implementations;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ISkuRepository> _skuRepositoryMock;
    private readonly Mock<IProductVariationOptionRepository> _productVariationOptionRepositoryMock;

    private readonly IProductService _productService;
    private readonly Mock<ILogger<ProductService>> _logger;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _skuRepositoryMock = new Mock<ISkuRepository>();
        _productVariationOptionRepositoryMock = new Mock<IProductVariationOptionRepository>();
        _logger = new Mock<ILogger<ProductService>>();

        _productService = new ProductService(
            _productRepositoryMock.Object,
            _skuRepositoryMock.Object,
            _productVariationOptionRepositoryMock.Object,
            _logger.Object);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Product_And_Return_Model()
    {
       // Arrange
       var productModel = new CreateProductModel
       {
           Id = 0,
           Name = "Test Product",
           Updated = DateTime.Now,
           Created = DateTime.Now,
           Description = "Test Description",
           ImageUrl = "testImageUrl",
           ProductTypeId = 1,
           Price = 11.99
       };

       _productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<CreateProductModel>())).ReturnsAsync(productModel);
       _productRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

       // Act
       var result = await _productService.AddAsync(productModel);

       // Assert
       Assert.NotNull(result);
       Assert.Equal(productModel.Name, result.Name);

       _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<CreateProductModel>()), Times.Once); // how many times is has been called
       _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Existing_Product()
    {
        // Arrange
        var productModel = new UpdateProductModel
        {
            Id = 1,
            Name = "Updated Product",
            Description = "Updated Description",
            ImageUrl = "updated-image.jpg",
            CategoryId = 2,
            ProductTypeId = 3
        };

        _productRepositoryMock.Setup(r => r.Update(It.IsAny<UpdateProductModel>()));
        _productRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _productService.UpdateAsync(productModel);

        // Assert
        _productRepositoryMock.Verify(r => r.Update(It.IsAny<UpdateProductModel>()), Times.Once);
        _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}