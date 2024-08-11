using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Dtos;
using EcommerceApp.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            
            await _productRepository.AddAsync(entity);
            await _productRepository.SaveChangesAsync();
        }

        public Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<ProductDto?> GetFirstOrDefaultAsync(Expression<Func<Product, bool>> filter)
        {
            return _productRepository.GetFirstOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(ProductDto entity)
        {
            _productRepository.Remove(entity);
            await _productRepository.SaveChangesAsync();
        }
    }
}
