using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Entities
{
    public class ProductVariation : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string Size { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public int Stock { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}