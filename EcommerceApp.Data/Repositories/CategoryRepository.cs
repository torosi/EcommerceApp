using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Mappings;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger)        
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(CreateCategoryModel category)
        {
            if (category is null) throw new ArgumentNullException(nameof(category));
            await _context.Categories.AddAsync(category.ToEntity());
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CategoryModel>> GetAllAsync(string? includeProperties = null, int? limit = null)
        {
            IQueryable<CategoryEntity> query = _context.Set<CategoryEntity>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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

            var entities = await query.ToListAsync();
            return entities.Select(x => x.ToDomain());
        }

        /// <inheritdoc />
        public void Remove(int id)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var model = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (model != null) throw new Exception($"Could not find Category with Id: {id}");

            _context.Categories.Remove(model!);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public void Update(UpdateCategoryModel category)
        {
            if (category is null) throw new ArgumentNullException(nameof(category));
            var entity = category.ToEntity();
            _context.Categories.Update(entity);
        }
    }
}