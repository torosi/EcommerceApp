using AutoMapper;
using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.MVC.Models.Category;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductType;
using EcommerceApp.MVC.Models.ProductVariationOption;
using EcommerceApp.MVC.Models.VariationType;

namespace EcommerceApp.MVC.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping from ProductViewModel to ProductModel
            CreateMap<ProductViewModel, ProductModel>()
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created ?? DateTime.Now))
               .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated ?? DateTime.Now));

            // Mapping from ProductModel to ProductViewModel
            CreateMap<ProductModel, ProductViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping from CateogryModel to CategoryViewModel
            CreateMap<CategoryModel, CategoryViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));
            
            // Mapping from CategoryViewModel to CategoryModel
            CreateMap<CategoryViewModel, CategoryModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping from ProductTypeViewModel to ProductTypeModel
            CreateMap<ProductTypeModel, ProductTypeViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping from ProductTypeViewModel to ProductTypeModel
            CreateMap<ProductTypeViewModel, ProductTypeModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => (DateTime?)src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (DateTime?)src.Updated));

            // Mapping from ProductVariationOptionModel to ProductVariationOptionViewModel
            CreateMap<ProductVariationOptionModel, ProductVariationOptionViewModel>();

            // Mapping from VariationTypeModel to CreateProductTypeViewModel
            CreateMap<VariationTypeModel, CreateVariationTypeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsSelected, opt => opt.Ignore());  // Default or handle separately

            // Reverse Mapping
            CreateMap<CreateVariationTypeViewModel, VariationTypeModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }

    }
}
