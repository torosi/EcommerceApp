﻿namespace EcommerceApp.MVC.Models.Product
{
    public class SkuViewModel
    {
        public int SkuId { get; set; }
        public string SkuString { get; set; }
        public int Quantity { get; set; }
        public List<VariationViewModel> Variations { get; set; }
    }
}