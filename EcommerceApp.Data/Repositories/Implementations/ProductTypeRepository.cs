using System;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;

namespace EcommerceApp.Data.Repositories.Implementations;

public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductTypeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(ProductType productType)
    {
        _context.ProductTypes.Update(productType);
    }
} 
