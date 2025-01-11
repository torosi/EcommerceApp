using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Service.Contracts;

public interface IVariationTypeService
{
    /// <summary>
    /// Method to get all VariationTypes
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task<IEnumerable<VariationTypeModel>> GetAllAsync(string? includeProperties = null);

    /// <summary>
    /// Method to create a new VariationType
    /// </summary>
    /// <param name="variationType"></param>
    /// <returns></returns>
    public Task CreateVariationTypeAsync(VariationTypeModel variationType);

    /// <summary>
    /// Method to get all VariationType by ProductTypeId
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <returns></returns>
    public Task<IEnumerable<VariationTypeModel>> GetAllByProductTypeAsync(int productTypeId); // this method is used to retrieve all of the variation types that are linked to a specific product type


    /// <summary>
    /// Method to create a new entry into the mappings table betweeen product type and variation type
    /// </summary>
    /// <param name="variationTypeIds"></param>
    /// <param name="productTypeId"></param>
    /// <returns></returns>
    public Task CreateProductTypeAndVariationTypeMappings(IEnumerable<int> variationTypeIds, int productTypeId);
}
