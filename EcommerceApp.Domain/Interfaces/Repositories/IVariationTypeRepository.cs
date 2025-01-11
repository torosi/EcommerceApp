using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IVariationTypeRepository
{
    public Task CreateProductTypeVariationMappingRangeAsync(IEnumerable<ProductTypeVariationMappingModel> mapping);
    public Task<IEnumerable<VariationTypeModel?>> GetAllByProductTypeAsync(int productTypeId);
    Task SaveChangesAsync();
    Task AddAsync(VariationTypeModel variationTypeModel);
    Task<IEnumerable<VariationTypeModel>> GetAllAsync(string? includeProperties = null);
}
