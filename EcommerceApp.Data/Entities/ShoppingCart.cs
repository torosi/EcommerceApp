using EcommerceApp.Data.Entities.Products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int SkuId { get; set; }
        [ForeignKey("SkuId")]
        [ValidateNever]
        public Sku Sku { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int Count { get; set; }
    }
}
