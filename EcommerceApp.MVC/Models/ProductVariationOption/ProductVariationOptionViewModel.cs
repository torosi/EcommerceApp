﻿namespace EcommerceApp.MVC.Models.ProductVariationOption
{
    public class ProductVariationOptionViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int SkuId { get; set; }
        public string SkuString { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int VariationTypeId { get; set; }
        public string VariationTypeName { get; set; }
        public string VariationValue { get; set; } = string.Empty;
    }
}
