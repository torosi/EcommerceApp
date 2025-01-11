using EcommerceApp.Domain.Models;
using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Models.Variations;

namespace EcommerceApp.Service.Contracts
{
    interface IProductService
    {
        /// <summary>
        /// A method to retrieve all product.
        /// </summary>
        /// <returns>A collection of products.</returns>
        Task<IEnumerable<ProductModel>> GetAllAsync(string? includeProperties = null);

        /// <summary>
        /// Method to retrieve first of detault ProductModel by passed in expression
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <param name="tracked"></param>
        /// <returns></returns>
        Task<ProductModel?> GetProductById(int productId, string? includeProperties = null);

        /// <summary>
        /// Method to add new ProductModel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ProductModel> AddAsync(ProductModel entity);

        /// <summary>
        /// Method to remove ProductModel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(int productId);

        /// <summary>
        /// Method to Update ProductModel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(ProductModel entity);

        /// <summary>
        /// Method to get collection of ProductModels that accepts pagination values to offset the returned results
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        Task<(int TotalCount, IEnumerable<ProductModel> Products)> SearchProductByCategoryId(int categoryId, string? includeProperties = null, int pageNumber = 1, int itemsPerPage = 20);

        /// <summary>
        /// Method to get a collection of SkuModels paired with their corresponding VariationModel values
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>IEnumerable<SkuWithVariationsModel></returns>
        Task<IEnumerable<SkuWithVariationsModel>> GetProductVariationsAsync(int productId);

        /// <summary>
        /// Method to create a new Sku and new ProductVariationOptions
        /// </summary>
        /// <param name="skus"></param>
        /// <param name="variations"></param>
        /// <returns></returns>
        Task CreateProductVariations(IEnumerable<SkuModel> skus, IEnumerable<ProductVariationOptionModel> variations);
    }
}
