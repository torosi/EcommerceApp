using EcommerceApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Dtos.Products
{
    public class SkuDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SkuString { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
