using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories.Implementations;

public class VariationTypeRepository : BaseRepository<VariationType>, IVariationTypeRepository
{
    private readonly ApplicationDbContext _context;

    public VariationTypeRepository(ApplicationDbContext context, ILogger<VariationTypeRepository> logger) : base(context, logger)
    {
        _context = context;
    }

    public async Task CreateProductTypeVariationMappingAsync(ProductTypeVariationMapping mapping)
    {
        await _context.ProductTypeVariationMappings.AddAsync(mapping);
        await _context.SaveChangesAsync();
    }

}
