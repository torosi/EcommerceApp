using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories;

public class SkuRepository : ISkuRepository
{
    private readonly ApplicationDbContext _context;
    public SkuRepository(ApplicationDbContext context, ILogger<SkuEntity> logger)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SkuModel>> AddRangeAndSaveAsync(IEnumerable<SkuModel> skus)
    {
        if (skus is null) throw new ArgumentNullException(nameof(skus));

        var skuEntities = skus.Select(x => x.ToEntity()).ToList();
        
        await _context.Skus.AddRangeAsync(skuEntities);
        await _context.SaveChangesAsync();

        var skuModels = skuEntities.Select(x => x.ToDomain());

        return skuModels;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings)
    {
        return await _context.Skus
            .Where(s => skuStrings.Contains(s.SkuString))
            .Select(x => x.ToDomain())
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<SkuModel?> GetSingleBySkuStringAsync(string skuString)
    {
        var skuEntity = await _context.Skus.SingleOrDefaultAsync(x => x.SkuString == skuString);
        if (skuEntity == null) return null;

        return skuEntity.ToDomain();
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
