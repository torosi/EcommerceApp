using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.ProductType;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductTypeRepository> _logger;

    public ProductTypeRepository(ApplicationDbContext context, ILogger<ProductTypeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> AddAndSaveAsync(CreateProductTypeModel productType)
    {
        if (productType is null) throw new ArgumentNullException(nameof(productType));

        var entity = productType.ToEntity(); // TODO: this only has access to this method because it implements the same class, we need to change it so that it is its own class without without Id and does not implement this. This will mean that it will need its own mapping method adding too.
        await _context.ProductTypes.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<ProductTypeModel>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<ProductTypeEntity> query = _context.Set<ProductTypeEntity>();

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        var results = await query.ToListAsync();
        _logger.LogTrace("Found '{entities}'", string.Join(", ", results.ToString()));

        return results.Select(x => x.ToDomain());
    }

    public async Task<ProductTypeModel?> GetProductTypeById(int id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id);
        var entity = await _context.ProductTypes.SingleOrDefaultAsync(c => c.Id == id);
        return entity == null ? null : entity!.ToDomain();
    }

    public void Remove(int id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id);

        var model = _context.ProductTypes.SingleOrDefault(c => c.Id == id);
        if (model == null) throw new Exception($"Could not find Product Type with Id: {id}");

        _context.ProductTypes.Remove(model!);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(UpdateProductTypeModel productType)
    {
        if (productType is null) throw new ArgumentNullException(nameof(productType));
        _context.ProductTypes.Update(productType.ToEntity());
    }
}
