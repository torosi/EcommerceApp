﻿using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;


namespace EcommerceApp.Test.Mappings
{
    public class ProductMappingsTests
    {
        [Fact]
        public void ToDto_Should_Map_Product_To_ProductDto_Correctly()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                ImageUrl = "testImage.jpg",
                CategoryId = 2,
                Category = new Category { Id = 2, Name = "Electronics" },
                ProductTypeId = 3,
                ProductType = new ProductType { Id = 3, Name = "Gadget" },
                Price = 100.0
            };

            // Act
            var dto = product.ToDto();

            // Assert
            Assert.Equal(product.Id, dto.Id);
            Assert.Equal(product.Name, dto.Name);
            Assert.Equal(product.Description, dto.Description);
            Assert.Equal(product.Created, dto.Created);
            Assert.Equal(product.Updated, dto.Updated);
            Assert.Equal(product.ImageUrl, dto.ImageUrl);
            Assert.Equal(product.CategoryId, dto.CategoryId);
            Assert.Equal(product.Category.Id, dto.Category.Id);
            Assert.Equal(product.ProductTypeId, dto.ProductTypeId);
            Assert.Equal(product.ProductType.Id, dto.ProductType.Id);
            Assert.Equal(product.Price, dto.Price);
            Assert.NotNull(dto.Category);
            Assert.NotNull(dto.ProductType);
        }

        [Fact]
        public void ToEntity_Should_Map_Product_To_Product_Correctly()
        {
            // Arrange
            var product = new ProductDto
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                ImageUrl = "testImage.jpg",
                CategoryId = 2,
                Category = new CategoryDto { Id = 2, Name = "Electronics" },
                ProductTypeId = 3,
                ProductType = new ProductTypeDto { Id = 3, Name = "Gadget" },
                Price = 100.0
            };

            // Act
            var entity = product.ToEntity();

            // Assert
            Assert.Equal(product.Id, entity.Id);
            Assert.Equal(product.Name, entity.Name);
            Assert.Equal(product.Description, entity.Description);
            Assert.Equal(product.Created, entity.Created);
            Assert.Equal(product.Updated, entity.Updated);
            Assert.Equal(product.ImageUrl, entity.ImageUrl);
            Assert.Equal(product.CategoryId, entity.CategoryId);
            Assert.Equal(product.ProductTypeId, entity.ProductTypeId);
            Assert.Equal(product.Price, entity.Price);
        }

    }
}