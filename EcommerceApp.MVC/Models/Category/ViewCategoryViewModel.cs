using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.MVC.Models.Product;

namespace EcommerceApp.MVC.Models.Category
{
    public class ViewCategoryViewModel
    {
        public CategoryViewModel Category { get; set; }
        public List<ProductViewModel>? Products { get; set; }
    }
}