using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Entities
{
    public class VariationAttribute : BaseEntity
    {
        public int ProductVariationId { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }

        public int VariationId { get; set; }
        public virtual Variation Variation { get; set; }
    }
}