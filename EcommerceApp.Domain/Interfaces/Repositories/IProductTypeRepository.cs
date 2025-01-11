using EcommerceApp.Domain.Models.Products;

namespace EcommerceApp.Domain.Interfaces.Repositories;

public interface IProductTypeRepository : IRepository<ProductTypeModel>
{
    /// <summary>
    /// Method to update product type
    /// </summary>
    /// <param name="productType"></param>
    public void Update(ProductTypeModel productType);
}
