using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Service.Contracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// A method to get all categories
        /// </summary>
        /// <returns>IEnumerable CategoryModel</returns>
        Task<IEnumerable<CategoryModel>> GetAllAsync(int? limit = null);

        /// <summary>
        /// A method to add a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        Task AddAsync(CategoryModel entity);

        /// <summary>
        /// A method to remove a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        Task RemoveAsync(CategoryModel entity);

        /// <summary>
        /// A method to update a single category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        Task UpdateAsync(CategoryModel entity);

        /// <summary>
        /// A method to get a single category by Id
        /// </summary>
        /// <param name="categoryId">Identifier for a category</param>
        /// <returns></returns>
        Task<CategoryModel?> GetCategoryById(int categoryId);

        /// <summary>
        /// A method to remove a category by Id
        /// </summary>
        /// <param name="categoryId">Identifier for a category</param>
        /// <returns>Task</returns>
        Task RemoveByIdAsync(int categoryId);
    }
}