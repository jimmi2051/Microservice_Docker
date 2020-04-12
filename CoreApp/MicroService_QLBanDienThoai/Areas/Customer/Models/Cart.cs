using MicroService_QLBanDienThoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Customer.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Cart_Name { get; set; }
        public int Product_Id { get; set; }
        public int quantity { get; set; }
    }
}
