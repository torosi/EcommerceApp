using System;
using EcommerceApp.Data.Entities.Products;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface ISkuRepository : IRepository<Sku>
{
    public Task<IEnumerable<Sku>> GetBySkuStringsAsync(IEnumerable<string> skuStrings);
    public Task AddRangeAsync(IEnumerable<Sku> skus);
}
