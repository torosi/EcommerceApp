using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories.Implementations;

public class VariationTypeRepository : BaseRepository<VariationType>, IVariationTypeRepository
{
    private readonly ApplicationDbContext _context;

    public VariationTypeRepository(ApplicationDbContext context, ILogger<VariationTypeRepository> logger) : base(context, logger)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task CreateProductTypeVariationMappingAsync(ProductTypeVariationMapping mapping)
    {
        await _context.ProductTypeVariationMappings.AddAsync(mapping);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationType?>> GetAllByProductTypeAsync(int productTypeId)
    {
        // Fetch the mappings including VariationType navigation property
        var productTypeVariationMappings = await _context.ProductTypeVariationMappings
            .Include(ptvm => ptvm.VariationType) // Use strongly-typed Include
            .Where(x => x.ProductTypeId == productTypeId)
            .ToListAsync();

        // Extract VariationType entities from the mappings
        var variationTypes = productTypeVariationMappings
            .Select(ptvm => ptvm.VariationType) // Select VariationType from mapping
            .Where(vt => vt != null) // Ensure no nulls are included
            .Distinct();    

        return variationTypes;
    }

}
