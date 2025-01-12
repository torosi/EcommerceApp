using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Service.Contracts;

namespace EcommerceApp.Service.Implementations;

public class SkuService : ISkuService
{
    private readonly ISkuRepository _skuRepository;
    public SkuService(ISkuRepository skuRepository)
    {
        _skuRepository = skuRepository;
    }

    public async Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings)
    {
        if (skuStrings is null) throw new ArgumentNullException(nameof(skuStrings));
        if (!skuStrings.Any()) throw new ArgumentException(nameof(skuStrings));

        var skus = await _skuRepository.GetBySkuStringsAsync(skuStrings);
        return skus;
    }

    public async Task<SkuModel?> GetSingleBySkuStringAsync(string skuString)
    {
        if (skuString is null) throw new ArgumentNullException(nameof(skuString));

        var sku = await _skuRepository.GetSingleBySkuStringAsync(skuString);
        return sku;
    }
}
