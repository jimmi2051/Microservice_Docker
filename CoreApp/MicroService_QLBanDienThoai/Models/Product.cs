using System;
using System.Collections.Generic;

namespace MicroService_QLBanDienThoai.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
        public int? IsVisible { get; set; }
        public int? IsActive { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
