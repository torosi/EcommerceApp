using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Dtos
{
    public class ProductVariationDto
    {
        public int ProductId { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public ProductDto Product { get; set; }
    }
}