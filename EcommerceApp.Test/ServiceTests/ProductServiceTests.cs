using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models;
using Moq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Service.Implementations;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Models.Products;

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
       var productModel = new CreateProductModel {
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


    //[Fact]
    //public async Task UpdateAsync_Should_Update_Existing_Product()
    //{
    //    // Arrange
    //    var productModel = new ProductModel
    //    {
    //        Id = 1,
    //        Name = "Updated Product",
    //        Description = "Updated Description",
    //        ImageUrl = "updated-image.jpg",
    //        CategoryId = 2,
    //        ProductTypeId = 3
    //    };

    //    var productEntity = new ProductEntity
    //    {
    //        Id = 1,
    //        Name = "Old Product",
    //        Description = "Old Description",
    //        ImageUrl = "old-image.jpg",
    //        CategoryId = 1,
    //        ProductTypeId = 1
    //    };

    //    _productRepositoryMock.Setup(r => r.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>(), true, null)) // this has to match what is being called so true null is the default parameters because the service class does not pass them in.
    //                          .ReturnsAsync(productEntity);
    //    _productRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

    //    // Act
    //    await _productService.UpdateAsync(productModel);

    //    // Assert
    //    Assert.Equal(productModel.Name, productEntity.Name);
    //    Assert.Equal(productModel.Description, productEntity.Description);
    //    Assert.Equal(productModel.ImageUrl, productEntity.ImageUrl);
    //    Assert.Equal(productModel.CategoryId, productEntity.CategoryId);
    //    Assert.Equal(productModel.ProductTypeId, productEntity.ProductTypeId);

    //    _productRepositoryMock.Verify(r => r.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>(), true, null), Times.Once);
    //    _productRepositoryMock.Verify(r => r.Update(It.IsAny<ProductEntity>()), Times.Once);
    //    _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    //}
}