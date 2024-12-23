﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceApp.Domain.Dtos.Products;

namespace EcommerceApp.Domain.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeDto? ProductType { get; set; }
        public double Price { get; set; }
    }
}
