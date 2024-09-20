using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Mappings;
using EcommerceApp.Domain.Services.Contracts;
using System.Linq.Expressions;

namespace EcommerceApp.Domain.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddAsync(ProductDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var productEntity = entity.ToEntity();

            await _productRepository.AddAsync(productEntity);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null)
        {
            var productEntities = await _productRepository.GetAllAsync(includeProperties, filter);
            return productEntities.Select(x => x.ToDto());
        }

        public async Task<ProductDto?> GetFirstOrDefaultAsync(Expression<Func<Product, bool>> filter)
        {
            var productEntity = await _productRepository.GetFirstOrDefaultAsync(filter);
            if (productEntity == null) return null;
            return productEntity.ToDto(); 
        }

        public async Task RemoveAsync(ProductDto entity)
        {
            if (entity == null) throw new ArgumentException(nameof(entity));
            _productRepository.Remove(entity.ToEntity());
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // 1) get the entity from db
            var productFromDb = await _productRepository.GetFirstOrDefaultAsync(x => x.Id == entity.Id);

            // 2) update entity
            if (productFromDb != null)
            {
                productFromDb.Description = entity.Description;
                productFromDb.Name = entity.Name;
                productFromDb.Updated = DateTime.Now;

                _productRepository.Update(productFromDb);
                await _productRepository.SaveChangesAsync();
            }
        }
    }
}
