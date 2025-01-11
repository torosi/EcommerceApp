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
    public async Task AddRangeAsync(IEnumerable<SkuModel> skus)
    {
        if (skus is null) throw new ArgumentNullException(nameof(skus));

        foreach (var skusItem in skus) 
        {
            await _context.Skus.AddAsync(skusItem.ToEntity());
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SkuModel>> GetBySkuStringsAsync(IEnumerable<string> skuStrings)
    {
        return await _context.Skus
            .Where(s => skuStrings.Contains(s.SkuString))
            .Select(x => x.ToDomain())
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
