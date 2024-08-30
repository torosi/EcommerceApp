using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;

namespace EcommerceApp.Domain.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Updated = category.Updated,
                Created = category.Created,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
            };
        }

        public static Category ToEntity(this CategoryDto categoryDto)
        {
            return new Category()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Updated = categoryDto.Updated,
                Created = categoryDto.Created,
                Description = categoryDto.Description,
                ImageUrl = categoryDto.ImageUrl,
            };
        }

    }
}