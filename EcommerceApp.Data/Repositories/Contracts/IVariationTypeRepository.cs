using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface IVariationTypeRepository : IRepository<VariationType>
{
    /// <summary>
    /// Method to create a new Product Type - Varition Type mapping
    /// </summary>
    /// <param name="variationType"></param>
    /// <param name="productTypeid"></param>
    /// <returns></returns>
    public Task CreateProductTypeVariationMappingRangeAsync(IEnumerable<ProductTypeVariationMapping> mapping);

    /// <summary>
    /// Method to get variation types by productTypeId
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <returns></returns>
    public Task<IEnumerable<VariationType?>> GetAllByProductTypeAsync(int productTypeId);
}
