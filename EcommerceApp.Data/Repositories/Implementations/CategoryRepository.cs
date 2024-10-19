using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? includeProperties = null, int? limit = null)
        {
            IQueryable<Category> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            // Order by name (or any relevant property) alphabetically
            query = query.OrderBy(c => c.Name);

            // Take the top x amount depending on what is passed in
            if (limit != null && limit != 0)
            {
                query = query.Take(limit.Value);
            }

            return await query.ToListAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}