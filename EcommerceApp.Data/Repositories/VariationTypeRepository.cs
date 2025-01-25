using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories;

public class VariationTypeRepository : IVariationTypeRepository
{
    private readonly ApplicationDbContext _context;

    public VariationTypeRepository(ApplicationDbContext context, ILogger<VariationTypeRepository> logger)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddAsync(CreateVariationTypeModel variationTypeModel)
    {
        if (variationTypeModel is null) throw new ArgumentNullException(nameof(variationTypeModel));
        await _context.VariationTypes.AddAsync(variationTypeModel.ToEntity());
    }

    /// <inheritdoc />
    public async Task CreateProductTypeVariationMappingRangeAsync(IEnumerable<ProductTypeVariationMappingModel> mappings)
    {
        if (mappings is null) throw new ArgumentNullException(nameof(mappings));
        if (!mappings.Any()) throw new ArgumentException(nameof(mappings));

        foreach (var mapping in mappings)
        {
            await _context.ProductTypeVariationMappings.AddAsync(mapping.ToEntity());
        }

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeModel>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<VariationTypeEntity> query = _context.Set<VariationTypeEntity>();

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        // Order by name (or any relevant property) alphabetically
        query = query.OrderBy(c => c.Name);

        var entities = await query.ToListAsync();
        return entities.Select(x => x.ToDomain());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<VariationTypeModel?>> GetAllByProductTypeAsync(int productTypeId)
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

        return variationTypes.Select(x => x.ToDomain());
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
