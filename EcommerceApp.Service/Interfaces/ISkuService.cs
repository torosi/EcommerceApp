using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Service.Contracts;

public interface ISkuService
{
    Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings);
    Task<SkuModel?> GetSingleBySkuStringAsync(string skuString);
}
