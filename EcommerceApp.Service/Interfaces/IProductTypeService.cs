using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Service.Contracts;

public interface IProductTypeService
{
    public Task<int> AddAsync(ProductTypeModel entity);
    public Task<IEnumerable<ProductTypeModel>> GetAllAsync();
    public Task<ProductTypeModel?> GetProductTypeByIdAsync(int id);
    public Task UpdateAsync(ProductTypeModel entity);
    public Task RemoveAsync(ProductTypeModel entity);

}
