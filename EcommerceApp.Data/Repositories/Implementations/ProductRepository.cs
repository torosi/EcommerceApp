using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null)
        {
            IQueryable<Product> query = _dbSet;

            // Apply filtering logic
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void UpdateRange(IEnumerable<Product> products)
        {
            if (products == null || !products.Any()) throw new ArgumentNullException(nameof(products));

            _context.Products.UpdateRange(products);
        }

    }
}
