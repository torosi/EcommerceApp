using System;
using EcommerceApp.Data.Entities.Products;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface IProductVariationOptionRepository : IRepository<ProductVariationOption>
{
    /// <summary>
    /// Method to add a range of ProductVariationOptions
    /// </summary>
    /// <param name="variations"></param>
    /// <returns></returns>
    public Task AddRangeAsync(IEnumerable<ProductVariationOption> variations);
}
