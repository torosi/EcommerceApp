using System.Linq.Expressions;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Domain.Models.Category;

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
        public async Task AddAsync(CategoryModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var category = entity.ToEntity();

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CategoryModel>> GetAllAsync(int? limit = null)
        {
            var categories = await _categoryRepository.GetAllAsync(limit: limit);
            var categoryModels = categories.Select(c => c.ToModel());
            return categoryModels;
        }

        /// <inheritdoc />
        public async Task<CategoryModel?> GetFirstOrDefaultAsync(Expression<Func<Category, bool>> filter, bool tracked = true)
        {
            var category = await _categoryRepository.GetFirstOrDefaultAsync(filter, tracked);
            if (category == null) return null;
            return category.ToModel();
        }

        /// <inheritdoc />
        public async Task RemoveAsync(CategoryModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));


            // we need to remove the categoryid from all of the products that currently have this category
            var products = await _productRepository.GetAllAsync(filter: x => x.CategoryId == entity.Id);
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

            _categoryRepository.Remove(entity.ToEntity());
            await _categoryRepository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(CategoryModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var entityFromDb = await _categoryRepository.GetFirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entityFromDb != null)
            {
                entityFromDb.Description = entity.Description;
                entityFromDb.Name = entity.Name;
                entityFromDb.Updated = DateTime.Now;
                entityFromDb.ImageUrl = entity.ImageUrl;

                _categoryRepository.Update(entityFromDb);
                await _categoryRepository.SaveChangesAsync();
            }
        }
    }
}