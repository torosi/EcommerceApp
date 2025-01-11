using EcommerceApp.Data.Entities.Products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    public class ShoppingCartEntity : BaseEntity
    {
        public int SkuId { get; set; }
        [ForeignKey("SkuId")]
        [ValidateNever]
        public SkuEntity Sku { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUserEntity ApplicationUser { get; set; }

        public int Count { get; set; }
    }
}
