using EcommerceApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product product);
        public void UpdateRange(IEnumerable<Product> products);
        public Task<IEnumerable<Product>> GetAllAsync(string? includeProperties = null, Expression<Func<Product, bool>>? filter = null);
    }
}
