using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Extentions.Mappings;

namespace EcommerceApp.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        /// <inheritdoc />
        public async Task AddAsync(CategoryModel category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            await _categoryRepository.AddAsync(category.ToCreateModel());
            await _categoryRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CategoryModel>> GetAllAsync(int? limit = null)
        {
            return await _categoryRepository.GetAllAsync(limit: limit);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(CategoryModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // we need to remove the categoryid from all of the products that currently have this category
            var productModels = await _productRepository.GetAllAsync();

            // if there are any products in this category, then we need to remove them from category first
            if (productModels.Any())
            {
                foreach (var product in productModels)
                {
                    product.CategoryId = null;
                }

                var updateProductModels = productModels.Select(p => p.ToUpdateModel());

                _productRepository.UpdateRange(updateProductModels);
                await _productRepository.SaveChangesAsync();
            }

            _categoryRepository.Remove(entity.Id);
            await _categoryRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(CategoryModel category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            _categoryRepository.Update(category.ToUpdateModel());
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<CategoryModel?> GetCategoryById(int categoryId)
        {
            ArgumentOutOfRangeException.ThrowIfZero(categoryId);

            var category = await _categoryRepository.GetCategoryById(categoryId);
            return category;
        }
    }
}