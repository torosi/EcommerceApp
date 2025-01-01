using EcommerceApp.Domain.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Dtos
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int SkuId { get; set; }
        public SkuWithVariationsDto? Sku { get; set; }
        public int Count { get; set; }
        public ProductDto? Product { get; set;}
    }
}
