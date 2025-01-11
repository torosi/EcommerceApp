using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IVariationTypeRepository : IRepository<VariationTypeModel>
{
    /// <summary>
    /// Method to create a new Product Type - Varition Type mapping
    /// </summary>
    /// <param name="variationType"></param>
    /// <param name="productTypeid"></param>
    /// <returns></returns>
    public Task CreateProductTypeVariationMappingRangeAsync(IEnumerable<ProductTypeVariationMappingModel> mapping);

    /// <summary>
    /// Method to get variation types by productTypeId
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <returns></returns>
    public Task<IEnumerable<VariationTypeModel?>> GetAllByProductTypeAsync(int productTypeId);
}
