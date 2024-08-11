using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Mappings
{
    public static class ImageMappings
    {
        public static Image ToEntity(this ImageDto image)
        {
            return new Image()
            {
                Id = image.Id,
                Created = image.Created,
                ImageData = image.ImageData,
                Updated = image.Updated,
            };
        }

        public static ImageDto ToDto(this Image image)
        {
            return new ImageDto()
            {
                Id = image.Id,
                Created = image.Created,
                ImageData = image.ImageData,
                Updated = image.Updated,
            };
        }
    }
}
