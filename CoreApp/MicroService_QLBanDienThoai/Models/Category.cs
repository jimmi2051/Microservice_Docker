using System;
using System.Collections.Generic;

namespace MicroService_QLBanDienThoai.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Quantity { get; set; }
        public int? IsActive { get; set; }
        public int? Archive { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
