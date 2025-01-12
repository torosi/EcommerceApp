using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProductTypeRepository
{
    public void Update(ProductTypeModel productType);
    Task<IEnumerable<ProductTypeModel>> GetAllAsync(string? includeProperties = null);
    Task<ProductTypeModel?> GetProductTypeById(int id);
    Task<int> AddAsync(ProductTypeModel model);
    void Remove(int id);
    Task SaveChangesAsync();
}
