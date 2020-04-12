using MicroService_QLBanDienThoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Customer.Models
{
    public class ViewCart
    {
        public Product Product_Id { get; set; }
        public int quantity { get; set; }
    }
}
