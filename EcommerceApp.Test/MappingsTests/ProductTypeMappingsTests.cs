﻿using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Data.Mappings;

namespace EcommerceApp.Test.MappingsTests
{
    public class ProductTypeMappingsTests
    {
        [Fact]
        public void ToModel_Should_Map_ProductType_To_ProductTypeModel_Correctly()
        {
            // Arrange
            var productType = new ProductTypeEntity()
            {
                Id = 12,
                Name = "Test Product Type",
                Description = "This is a test",
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            // Act
            var productTypeModel = productType.ToDomain();

            // Assert
            Assert.Equal(productType.Id, productTypeModel.Id);
            Assert.Equal(productType.Name, productTypeModel.Name);
            Assert.Equal(productType.Created, productTypeModel.Created);
            Assert.Equal(productType.Updated, productTypeModel.Updated);
            Assert.Equal(productType.Description, productTypeModel.Description);
        }

        [Fact]
        public void ToEntity_Should_Map_ProductTypeModel_To_ProductType_Correctly()
        {
            // Arrange
            var productTypeModel = new ProductTypeModel()
            {
                Id = 87,
                Name = "Test Product Type Model",
                Description = "This is a test",
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            // Act
            var productType = productTypeModel.ToEntity();

            // Assert
            Assert.Equal(productType.Id, productTypeModel.Id);
            Assert.Equal(productType.Name, productTypeModel.Name);
            Assert.Equal(productType.Created, productTypeModel.Created);
            Assert.Equal(productType.Updated, productTypeModel.Updated);
            Assert.Equal(productType.Description, productTypeModel.Description);
        }


    }
}
