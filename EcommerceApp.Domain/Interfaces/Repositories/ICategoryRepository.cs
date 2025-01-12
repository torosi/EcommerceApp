using EcommerceApp.Domain.Models.Category;

namespace EcommerceApp.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    { 
        /// <summary>
        /// Method to add new category
        /// </summary>
        /// <param name="category">A <see cref="CreateCategoryModel"/></param>
        /// <returns></returns>
        Task AddAsync(CreateCategoryModel category);

        /// <summary>
        /// Method to remove category
        /// </summary>
        /// <param name="id">Identifier for the category to remove</param>
        void Remove(int id);

        /// <summary>
        /// Method to save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Method to update category
        /// </summary>
        /// <param name="category"></param>
        void Update(UpdateCategoryModel category);

        /// <summary>
        /// Method to get all categories
        /// </summary>
        /// <param name="includeProperties">Accepts string to include properties (ef)</param>
        /// <param name="limit">Accepts int value to limit the number of results to X amount</param>
        /// <returns></returns>
        Task<IEnumerable<CategoryModel>> GetAllAsync(string? includeProperties = null, int? limit = null);

        /// <summary>
        /// Method to get a single category by Id
        /// </summary>
        /// <param name="categoryId">Identifier for a catgeory</param>
        /// <returns></returns>
        Task<CategoryModel?> GetCategoryById(int categoryId);
    }
}