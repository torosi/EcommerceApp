using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;

namespace EcommerceApp.Test.MappingsTests
{
    public class ProductTypeMappingsTests
    {
        [Fact]
        public void ToDto_Should_Map_ProductType_To_ProductTypeDto_Correctly()
        {
            var productType = new ProductType()
            {
                Id = 12,
                Name = "Test Product Type",
                Description = "This is a test",
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            var productTypeDto = productType.ToDto();

            Assert.Equal(productType.Id, productTypeDto.Id);
            Assert.Equal(productType.Name, productTypeDto.Name);
            Assert.Equal(productType.Created, productTypeDto.Created);
            Assert.Equal(productType.Updated, productTypeDto.Updated);
            Assert.Equal(productType.Description, productTypeDto.Description);
        }

        [Fact]
        public void ToEntity_Should_Map_ProductTypeDto_To_ProductType_Correctly()
        {
            var productTypeDto = new ProductTypeDto()
            {
                Id = 87,
                Name = "Test Product Type Dto",
                Description = "This is a test",
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            var productType = productTypeDto.ToEntity();

            Assert.Equal(productType.Id, productTypeDto.Id);
            Assert.Equal(productType.Name, productTypeDto.Name);
            Assert.Equal(productType.Created, productTypeDto.Created);
            Assert.Equal(productType.Updated, productTypeDto.Updated);
            Assert.Equal(productType.Description, productTypeDto.Description);
        }


    }
}
