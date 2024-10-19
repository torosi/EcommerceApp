using System;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Data.Repositories.Contracts;

public interface IProductTypeRepository : IRepository<ProductType>
{
    public void Update(ProductType productType);
}
