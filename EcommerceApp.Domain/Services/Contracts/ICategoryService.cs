using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;
using EcommerceApp.Domain.Dtos;

namespace EcommerceApp.Domain.Services.Contracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// A method to get all categories
        /// </summary>
        /// <returns>IEnumerable CategoryDto</returns>
        public Task<IEnumerable<CategoryDto>> GetAllAsync();

        /// <summary>
        /// A method to get the first of default category
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>CategoryDto</returns>
        public Task<CategoryDto?> GetFirstOrDefaultAsync(Expression<Func<Category, bool>> filter);

        /// <summary>
        /// A method to add a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public Task AddAsync(CategoryDto entity);

        /// <summary>
        /// A method to remove a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public Task RemoveAsync(CategoryDto entity);

        /// <summary>
        /// A method to update a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public Task UpdateAsync(CategoryDto entity);
    }
}