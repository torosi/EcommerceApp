using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Products;
using System.Linq.Expressions;

namespace EcommerceApp.Service.Contracts;

public interface IProductTypeService
{
    /// <summary>
    /// Method to add a new product type
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<int> AddAsync(ProductTypeModel entity);

    /// <summary>
    /// Method to get all product types
    /// </summary>
    /// <returns>A collection of <see cref="ProductTypeModel"/></returns>
    public Task<IEnumerable<ProductTypeModel>> GetAllAsync();

    /// <summary>
    /// Method to get the first of default product type
    /// </summary>
    /// <param name="filter">Expression to filter product types</param>
    /// <param name="includeProperties">Include properties for foreign key relationships</param>
    /// <param name="tracked">Tracked from entity framework</param>
    /// <returns></returns>
    public Task<ProductTypeModel?> GetFirstOrDefaultAsync(Expression<Func<ProductType, bool>> filter, string? includeProperties = null, bool tracked = true);

    /// <summary>
    /// Method to update a product type
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task UpdateAsync(ProductTypeModel entity);

    /// <summary>
    /// Method to remove a product type
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task RemoveAsync(ProductTypeModel entity);

}
