using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;

namespace EcommerceApp.Domain.Services.Implementations;

public class SkuService : ISkuService
{
    private readonly ISkuRepository _skuRepository;
    public SkuService(ISkuRepository skuRepository)
    {
        _skuRepository = skuRepository;
    }


    public async Task<SkuDto?> GetFirstOrDefaultAsync(Expression<Func<Sku, bool>> filter, bool tracked = true)
    {
        var sku = await _skuRepository.GetFirstOrDefaultAsync(filter, tracked);
        if (sku == null) return null;
        return sku.ToDto();
    }
}
