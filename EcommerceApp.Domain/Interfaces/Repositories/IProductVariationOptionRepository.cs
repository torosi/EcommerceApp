using EcommerceApp.Domain.Models.Variations;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProductVariationOptionRepository
{
    public Task AddRangeAsync(IEnumerable<CreateProductVariationOptionModel> variations);
    Task SaveChangesAsync();
}
