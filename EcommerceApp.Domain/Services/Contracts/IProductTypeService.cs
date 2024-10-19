using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Services.Contracts;

public interface IProductTypeService
{
    public Task AddAsync(ProductTypeDto productTypeDto);

    /// <summary>
    /// Method to get all product types
    /// </summary>
    /// <returns>A collection of <see cref="ProductTypeDto"/></returns>
    Task<IEnumerable<ProductTypeDto>> GetAllAsync();
}
