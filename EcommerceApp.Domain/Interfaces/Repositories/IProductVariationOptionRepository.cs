using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProductVariationOptionRepository
{
    public Task AddRangeAsync(IEnumerable<ProductVariationOptionModel> variations);
    Task SaveChangesAsync();
}
