using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Services.Contracts;

public interface IVariationTypeService
{
    /// <summary>
    /// Method to get all VariationTypes
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task<IEnumerable<VariationTypeDto>> GetAllAsync(string? includeProperties = null, Expression<Func<VariationType, bool>>? filter = null);

    /// <summary>
    /// Method to create a new VariationType
    /// </summary>
    /// <param name="variationType"></param>
    /// <returns></returns>
    public Task CreateVariationTypeAsync(VariationTypeDto variationType);

    /// <summary>
    /// Method to get all VariationType by ProductTypeId
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <returns></returns>
    public Task<IEnumerable<VariationTypeDto>> GetAllByProductTypeAsync(int productTypeId); // this method is used to retrieve all of the variation types that are linked to a specific product type
}
