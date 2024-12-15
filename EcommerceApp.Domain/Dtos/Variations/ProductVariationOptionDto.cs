using EcommerceApp.Data.Entities.Products;
using EcommerceApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Dtos.Products
{
    public class ProductVariationOptionDto
    {
        public int Id { get; set; }
        public int SkuId { get; set; }
        public int VariationTypeId { get; set; }
        public string VariationTypeName { get; set; }= string.Empty;
        public string VariationValue { get; set; } = string.Empty;
    }
}
