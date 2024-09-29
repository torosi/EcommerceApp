using EcommerceApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = true, string? includeProperties = null);
        public Task AddAsync(T entity);
        public void Remove(T entity);
        public Task SaveChangesAsync();
    }
}
