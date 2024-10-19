using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> _dbSet; // i think that this needs to be internal rather than private because inheriting classes may use?
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ApplicationDbContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            this._logger = logger;
            this._dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null) // you could also add an expression filter here to get a filtered collection -- look at bulky project
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new Char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            var results = await query.ToListAsync();
            _logger.LogTrace("Found '{entities}'", string.Join(", ", results.ToString()));

            return results;
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.Where(filter).FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }




        // use this method for reference on how to include properties and filters etc

        /* public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null) // optional properties to include
        {
            filter ??= x => true;

            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            query = query.Where(filter);

            return await query.ToListAsync();
        }*/
    }
}
