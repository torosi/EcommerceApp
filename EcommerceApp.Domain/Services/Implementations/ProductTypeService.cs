using System;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;

namespace EcommerceApp.Domain.Services.Implementations;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;

    public ProductTypeService(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }


    public async Task AddAsync(ProductTypeDto productTypeDto)
    {
        if (productTypeDto == null) throw new ArgumentNullException(nameof(productTypeDto));

        var productEntity = productTypeDto.ToEntity();

        await _productTypeRepository.AddAsync(productEntity);
    }
}
