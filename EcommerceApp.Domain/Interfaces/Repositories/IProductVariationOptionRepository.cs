using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProductVariationOptionRepository : IRepository<ProductVariationOptionModel>
{
    /// <summary>
    /// Method to add a range of ProductVariationOptions
    /// </summary>
    /// <param name="variations"></param>
    /// <returns></returns>
    public Task AddRangeAsync(IEnumerable<ProductVariationOptionModel> variations);
}
