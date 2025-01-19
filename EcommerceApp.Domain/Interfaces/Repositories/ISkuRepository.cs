using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface ISkuRepository
{
    Task<SkuModel?> GetSingleBySkuStringAsync(string skuString);
    Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings);
    Task<IEnumerable<SkuModel>> AddRangeAndSaveAsync(IEnumerable<SkuModel> skus);
    Task SaveChangesAsync();
}
