using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Models
{
    public class Image : BaseEntity
    {
        public byte[] ImageData { get; set; }
    }
}