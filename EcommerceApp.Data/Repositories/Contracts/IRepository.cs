using System.Linq.Expressions;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = true, string? includeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
