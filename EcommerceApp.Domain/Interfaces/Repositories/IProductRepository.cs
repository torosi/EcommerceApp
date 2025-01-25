using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;
using System.Linq.Expressions;

namespace EcommerceApp.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Method to update product
        /// </summary>
        /// <param name="product"></param>
        void Update(UpdateProductModel product);

        /// <summary>
        /// Method to update multiple products
        /// </summary>
        /// <param name="products"></param>
        void UpdateRange(IEnumerable<UpdateProductModel> products);

        /// <summary>
        /// Method to get all products
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductModel>> GetAllAsync(string? includeProperties = null);

        /// <summary>
        /// Method to get products filtered by expression.
        /// Accepts arguments to allow for pagination
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter">Accepts expression to filter desired products</param>
        /// <param name="pageNumber"></param> // TODO: change these paginaition arguments to be less specific, i.e. change name to offset instead of page number and change items by page to limit
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        Task<(int TotalCount, IEnumerable<ProductModel> Products)> SearchProductByCategoryId(int categoryId, string? includeProperties = null, int pageNumber = 1, int itemsPerPage = 20);

        /// <summary>
        /// Method to get product variations
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<IEnumerable<SkuWithVariationsModel>> GetProductVariationsAsync(int productId);


        /// <summary>
        /// Method to get first of default entity T
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tracked"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<ProductModel?> GetProductById(int productId = 0, bool tracked = true, string? includeProperties = null);

        /// <summary>
        /// Method to add new entity T
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ProductModel> AddAsync(CreateProductModel product);

        /// <summary>
        /// Method to remove product
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);

        /// <summary>
        /// Method to save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

    }
}
