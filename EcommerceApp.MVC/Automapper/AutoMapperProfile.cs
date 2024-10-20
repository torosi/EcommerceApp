using AutoMapper;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.MVC.Models.Product;

namespace EcommerceApp.MVC.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping from ProductViewModel to ProductDto
            CreateMap<ProductViewModel, ProductDto>()
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created ?? DateTime.Now))
               .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated ?? DateTime.Now));

            // Mapping from ProductDto to ProductViewModel
            CreateMap<ProductDto, ProductViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping from CateogryDto to CategoryViewModel
            CreateMap<CategoryDto, CategoryViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));
            
            // Mapping from CategoryViewModel to CategoryDto
            CreateMap<CategoryViewModel, CategoryDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping from ProductTypeViewModel to ProductTypeDto
            CreateMap<ProductTypeDto, ProductTypeViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));
        }

    }
}
