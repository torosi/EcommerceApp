using AutoMapper;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.MVC.Models;
using EcommerceApp.MVC.Models.Product;

namespace EcommerceApp.MVC.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductViewModel, ProductDto>()
           .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created ?? DateTime.Now))
           .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated ?? DateTime.Now));

            // Mapping from ProductDto to ProductViewModel
            CreateMap<ProductDto, ProductViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping for Image
            CreateMap<ImageViewModel, ImageDto>().ReverseMap();
        }

    }
}
