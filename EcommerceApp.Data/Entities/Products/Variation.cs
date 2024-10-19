using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Entities
{
    public class Variation : BaseEntity
    {
        public int VariationTypeId { get; set; }
        public virtual VariationType VariationType { get; set; }

        public int VariationValueId { get; set; }
        public virtual VariationValue VariationValue { get; set; }
    }
}
