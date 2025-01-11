using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Service.Contracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// A method to get all categories
        /// </summary>
        /// <returns>IEnumerable CategoryModel</returns>
        public Task<IEnumerable<CategoryModel>> GetAllAsync(int? limit = null);

        /// <summary>
        /// A method to get the first of default category
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>CategoryModel</returns>
        public Task<CategoryModel?> GetFirstOrDefaultAsync(Expression<Func<Category, bool>> filter, bool tracked = true);

        /// <summary>
        /// A method to add a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public Task AddAsync(CategoryModel entity);

        /// <summary>
        /// A method to remove a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public Task RemoveAsync(CategoryModel entity);

        /// <summary>
        /// A method to update a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public Task UpdateAsync(CategoryModel entity);
    }
}