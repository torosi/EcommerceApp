using System.Linq.Expressions;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Method to get all entities T
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);

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
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Method to remove entity T
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// Method to save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
