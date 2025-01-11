using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface ISkuRepository : IRepository<SkuModel>
{
    /// <summary>
    /// Method to get Skus by SkuStrings
    /// </summary>
    /// <param name="skuStrings"></param>
    /// <returns></returns>
    public Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings);

    /// <summary>
    /// Method to add a range of Skus
    /// </summary>
    /// <param name="skus"></param>
    /// <returns></returns>
    public Task AddRangeAsync(IEnumerable<SkuModel> skus);
}
