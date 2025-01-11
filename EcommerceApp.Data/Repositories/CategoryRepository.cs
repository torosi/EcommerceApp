using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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
        public Task AddAsync(CreateCategoryModel category)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CategoryModel>> GetAllAsync(string? includeProperties = null, int? limit = null)
        {
            IQueryable<Category> query = _context.Set<Category>();

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

            // todo: map to domain model
            return await query.ToListAsync();
        }

        public Task<CategoryModel?> GetFirstOrDefaultAsync(Expression<Func<CategoryModel, bool>> filter, bool tracked = true, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Update(UpdateCategoryModel category)
        {
            // todo: map to entity and pass into below
            _context.Categories.Update(category);
        }

    }
}