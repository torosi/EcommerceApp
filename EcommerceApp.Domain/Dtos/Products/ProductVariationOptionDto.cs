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
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int SkuId { get; set; }
        public string SkuString { get; set; }
        public int Quantity { get; set; }
        public string VariationTypeName { get; set; }
        public string VariationValueName { get; set; }

    }
}
