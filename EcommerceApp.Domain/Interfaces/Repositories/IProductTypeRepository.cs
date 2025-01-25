using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.ProductType;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProductTypeRepository
{
    public void Update(UpdateProductTypeModel productType);
    Task<IEnumerable<ProductTypeModel>> GetAllAsync(string? includeProperties = null);
    Task<ProductTypeModel?> GetProductTypeById(int id);
    Task<int> AddAndSaveAsync(CreateProductTypeModel model);
    void Remove(int id);
    Task SaveChangesAsync();
}
