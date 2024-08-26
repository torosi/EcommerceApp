using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;

namespace EcommerceApp.Domain.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository =  categoryRepository;
        }

        public async Task AddAsync(CategoryDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var category = entity.ToEntity();

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = categories.Select(c => c.ToDto());
            return categoryDtos;
        }

        public async Task<CategoryDto?> GetFirstOrDefaultAsync(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryRepository.GetFirstOrDefaultAsync(filter);
            if (category == null) return null;
            return category.ToDto();
        }

        public async Task RemoveAsync(CategoryDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _categoryRepository.Remove(entity.ToEntity());
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var entityFromDb = await _categoryRepository.GetFirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entityFromDb != null)
            {
                entityFromDb.Description = entity.Description;
                entityFromDb.Name = entity.Name;
                entityFromDb.Updated = DateTime.Now;

                _categoryRepository.Update(entityFromDb);
                await _categoryRepository.SaveChangesAsync();
            }
        }
    }
}