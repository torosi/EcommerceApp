using System.Linq.Expressions;

namespace EcommerceApp.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Method to get all entities T
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null, Expression<Func<T, bool>>? filter = null);

        /// <summary>
        /// Method to get first of default entity T
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tracked"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = true, string? includeProperties = null);

        /// <summary>
        /// Method to add new entity T
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddAsync(T model);

        /// <summary>
        /// Method to remove entity T
        /// </summary>
        /// <param name="model"></param>
        void Remove(T model);

        /// <summary>
        /// Method to save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
