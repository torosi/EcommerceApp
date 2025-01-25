using AutoMapper;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.WebAPI.DTOs.Category;

namespace EcommerceApp.WebAPI.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryModel, CategoryDto>();
            CreateMap<CategoryDto, CategoryModel>();

        }
    }
}

