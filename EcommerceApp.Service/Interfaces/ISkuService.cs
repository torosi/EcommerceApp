using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Service.Contracts;

public interface ISkuService
{
    /// <summary>
    /// A method to get the first of default sku
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>SkuModel</returns>
    public Task<SkuModel?> GetFirstOrDefaultAsync(Expression<Func<Sku, bool>> filter, bool tracked = true);
}
