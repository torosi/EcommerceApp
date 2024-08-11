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
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        //navigation property for many to many
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}