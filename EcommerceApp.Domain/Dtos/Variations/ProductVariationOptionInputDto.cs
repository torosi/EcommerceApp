using System;

namespace EcommerceApp.Domain.Dtos.Variations;

public class ProductVariationOptionInputDto
{
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SkuString { get; set; } = string.Empty;
        public int VariationTypeId { get; set; }
        public string VariationValue { get; set; } = string.Empty;
}
