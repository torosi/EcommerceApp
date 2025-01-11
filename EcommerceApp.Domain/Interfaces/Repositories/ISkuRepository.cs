using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface ISkuRepository
{
    public Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings);
    public Task AddRangeAsync(IEnumerable<SkuModel> skus);
    Task SaveChangesAsync();
}
