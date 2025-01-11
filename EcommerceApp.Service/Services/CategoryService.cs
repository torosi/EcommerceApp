using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.Domain.Interfaces.Repositories;

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
            if (limit is null) throw new ArgumentNullException(nameof(limit));
            return await _categoryRepository.GetAllAsync(limit: limit);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(CategoryModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // we need to remove the categoryid from all of the products that currently have this category
            var products = await _productRepository.GetAllAsync();

            // if there are any products in this category, then we need to remove them from category first
            if (products.Any())
            {
                foreach (var product in products)
                {
                    product.CategoryId = null;
                }

                _productRepository.UpdateRange(products);
                await _productRepository.SaveChangesAsync();
            }

            _categoryRepository.Remove(entity.Id);
            await _categoryRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(CategoryModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _categoryRepository.Update(entity.ToUpdateModel());
            await _categoryRepository.SaveChangesAsync();
        }
    }
}