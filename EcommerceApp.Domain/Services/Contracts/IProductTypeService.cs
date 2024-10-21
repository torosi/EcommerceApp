using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using System.Linq.Expressions;

namespace EcommerceApp.Domain.Services.Contracts;

public interface IProductTypeService
{
    /// <summary>
    /// Method to add a new product type
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task AddAsync(ProductTypeDto entity);

    /// <summary>
    /// Method to get all product types
    /// </summary>
    /// <returns>A collection of <see cref="ProductTypeDto"/></returns>
    public Task<IEnumerable<ProductTypeDto>> GetAllAsync();

    /// <summary>
    /// Method to get the first of default product type
    /// </summary>
    /// <param name="filter">Expression to filter product types</param>
    /// <param name="includeProperties">Include properties for foreign key relationships</param>
    /// <param name="tracked">Tracked from entity framework</param>
    /// <returns></returns>
    public Task<ProductTypeDto?> GetFirstOrDefaultAsync(Expression<Func<ProductType, bool>> filter, string? includeProperties = null, bool tracked = true);
    
    /// <summary>
    /// Method to update a product type
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task UpdateAsync(ProductTypeDto entity);
}
