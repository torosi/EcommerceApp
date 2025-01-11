using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Products;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories;

public class ProductVariationOptionRepository : IProductVariationOptionRepository
{
    private readonly ApplicationDbContext _context;

    public ProductVariationOptionRepository(ApplicationDbContext context, ILogger<BaseRepository<ProductVariationOptionModel>> logger)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<ProductVariationOptionModel> variations)
    {
        if (variations is null) throw new ArgumentNullException(nameof(variations));

        foreach (var variation in variations)
        {
            await _context.ProductVariationOptions.AddAsync(variation.ToEntity());
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
