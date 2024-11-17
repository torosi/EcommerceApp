using System;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Services.Contracts;

public interface IVariationTypeService
{
    public Task<IEnumerable<VariationTypeDto>> GetAllAsync();
}
