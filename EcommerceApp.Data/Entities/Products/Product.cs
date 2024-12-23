using EcommerceApp.Data.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; } // navigation property

        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

    }
}