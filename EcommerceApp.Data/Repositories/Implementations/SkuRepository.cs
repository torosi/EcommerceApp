using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories.Implementations;

public class SkuRepository : BaseRepository<Sku>, ISkuRepository
{
    private readonly ApplicationDbContext _context;
    public SkuRepository(ApplicationDbContext context, ILogger<BaseRepository<Sku>> logger) : base(context, logger)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<Sku> skus)
    {
        await _context.Skus.AddRangeAsync(skus);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sku>> GetBySkuStringsAsync(IEnumerable<string> skuStrings)
    {
        return await _context.Skus
            .Where(s => skuStrings.Contains(s.SkuString))
            .ToListAsync();
    }

}
