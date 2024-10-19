using System;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Services.Contracts;

public interface IProductTypeService
{
    public Task AddAsync(ProductTypeDto productTypeDto);
}
