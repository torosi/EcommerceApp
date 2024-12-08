using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories.Implementations;

public class ProductTypeRepository : BaseRepository<ProductType>, IProductTypeRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductTypeRepository(ApplicationDbContext context, ILogger<ProductTypeRepository> logger) : base(context, logger)
    {
        _context = context;
    }

    /// <inheritdoc />
    public void Update(ProductType productType)
    {
        _context.ProductTypes.Update(productType);
    }
} 
