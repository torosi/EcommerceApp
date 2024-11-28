using System;
using EcommerceApp.Data.Entities.Products;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface IProductVariationOptionRepository : IRepository<ProductVariationOption>
{
    public Task AddRangeAsync(IEnumerable<ProductVariationOption> variations);
}
