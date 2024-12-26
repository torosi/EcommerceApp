using System;
using System.Linq.Expressions;
using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Services.Contracts;

public interface ISkuService
{
        /// <summary>
        /// A method to get the first of default sku
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>SkuDto</returns>
        public Task<SkuDto?> GetFirstOrDefaultAsync(Expression<Func<Sku, bool>> filter, bool tracked = true);
}
