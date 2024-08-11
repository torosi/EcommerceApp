using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Data.Entities
{
    public class Image : BaseEntity
    {
        public byte[] ImageData { get; set; }
        
        //navigation property for many to many
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}