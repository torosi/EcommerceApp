using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Mappings
{
    public static class ProductMappings
    {

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                Image = product.Image != null ? product.Image.ToDto() : null, // if the image is null we dont need to map it
                ImageId = product.ImageId,
            };
        }

        public static Product ToEntity(this ProductDto product)
        {
            return new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                Updated = product.Updated,
                Image = product.Image != null ? product.Image.ToEntity() : null,
                ImageId = product.ImageId,
            };
        }


    }
}
