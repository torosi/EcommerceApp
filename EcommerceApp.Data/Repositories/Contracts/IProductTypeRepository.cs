using System;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface IProductTypeRepository : IRepository<ProductType>
{
    /// <summary>
    /// Method to update product type
    /// </summary>
    /// <param name="productType"></param>
    public void Update(ProductType productType);
}
