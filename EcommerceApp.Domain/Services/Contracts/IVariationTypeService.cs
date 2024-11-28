using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Services.Contracts;

public interface IVariationTypeService
{
    public Task<IEnumerable<VariationTypeDto>> GetAllAsync(string? includeProperties = null, Expression<Func<VariationType, bool>>? filter = null);
    public Task CreateVariationTypeAsync(VariationTypeDto variationType, int productTypeId);
    public Task<IEnumerable<VariationTypeDto>> GetAllByProductTypeAsync(int productTypeId);
}
