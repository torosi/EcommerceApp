using System;
using EcommerceApp.Data.Entities.Products;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface ISkuRepository : IRepository<Sku>
{
    /// <summary>
    /// Method to get Skus by SkuStrings
    /// </summary>
    /// <param name="skuStrings"></param>
    /// <returns></returns>
    public Task<IEnumerable<Sku>> GetBySkuStringsAsync(IEnumerable<string> skuStrings);

    /// <summary>
    /// Method to add a range of Skus
    /// </summary>
    /// <param name="skus"></param>
    /// <returns></returns>
    public Task AddRangeAsync(IEnumerable<Sku> skus);
}
