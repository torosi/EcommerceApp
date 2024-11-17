using System;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Variations;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface IVariationTypeRepository : IRepository<VariationType>
{
    /// <summary>
    /// Method to create a new Product Type - Varition Type mapping
    /// </summary>
    /// <param name="variationType"></param>
    /// <param name="productTypeid"></param>
    /// <returns></returns>
    public Task CreateProductTypeVariationMappingAsync(ProductTypeVariationMapping mapping);
}
