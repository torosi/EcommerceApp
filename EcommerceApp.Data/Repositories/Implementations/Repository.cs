using EcommerceApp.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> _dbSet; // i think that this needs to be internal rather than private because inheriting classes may use?
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
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

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
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
