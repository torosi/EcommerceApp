using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Method to update category
        /// </summary>
        /// <param name="category"></param>
        public void Update(Category category);

        /// <summary>
        /// Method to get all categories
        /// </summary>
        /// <param name="includeProperties">Accepts string to include properties (ef)</param>
        /// <param name="limit">Accepts int value to limit the number of results to X amount</param>
        /// <returns></returns>
        public Task<IEnumerable<Category>> GetAllAsync(string? includeProperties = null, int? limit = null);
    }
}