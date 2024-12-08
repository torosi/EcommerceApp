using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Dtos.Products;
using EcommerceApp.Domain.Dtos.Variations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Services.Contracts
{
    public interface IProductService
    {
        /// <summary>
        /// A method to retrieve all product.
        /// </summary>
        /// <returns>A collection of products.</returns>
        public Task<IEnumerable<ProductDto>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null);

        /// <summary>
        /// Method to retrieve first of detault ProductDto by passed in expression
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <param name="tracked"></param>
        /// <returns></returns>
        public Task<ProductDto?> GetFirstOrDefaultAsync(Expression<Func<Product, bool>> filter, string? includeProperties = null, bool tracked = true);

        /// <summary>
        /// Method to add new ProductDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<ProductDto> AddAsync(ProductDto entity);

        /// <summary>
        /// Method to remove ProductDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task RemoveAsync(ProductDto entity);

        /// <summary>
        /// Method to Update ProductDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(ProductDto entity);

        /// <summary>
        /// Method to get collection of ProductDtos that accepts pagination values to offset the returned results
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public Task<(int TotalCount, IEnumerable<ProductDto> Products)> GetFilteredProductsAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null, int pageNumber = 1, int itemsPerPage = 20);

        /// <summary>
        /// Method to get a collection of SkuDtos paired with their corresponding VariationDto values
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>IEnumerable<SkuWithVariationsDto></returns>
        public Task<IEnumerable<SkuWithVariationsDto>> GetProductVariationsAsync(int productId);

        /// <summary>
        /// Method to create a new Sku and new ProductVariationOptions
        /// </summary>
        /// <param name="skus"></param>
        /// <param name="variations"></param>
        /// <returns></returns>
        public Task CreateProductVariations(IEnumerable<SkuDto> skus, IEnumerable<ProductVariationOptionInputDto> variations);
    }
}
