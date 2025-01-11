using System;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories;

public class ProductVariationOptionRepository : BaseRepository<ProductVariationOption>, IProductVariationOptionRepository
{
    private readonly ApplicationDbContext _context;
    public ProductVariationOptionRepository(ApplicationDbContext context, ILogger<BaseRepository<ProductVariationOption>> logger) : base(context, logger)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<ProductVariationOption> variations)
    {
        await _context.ProductVariationOptions.AddRangeAsync(variations);
    }
}
