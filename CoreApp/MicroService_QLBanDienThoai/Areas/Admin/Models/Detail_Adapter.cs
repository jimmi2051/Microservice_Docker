using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Admin.Models
{
    public class Detail_Adapter
    {
        public int ID { get; set; }
        public int Receipt_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public int State_Product { get; set; }


        // Nhung thuoc tinh co lien toi bang khac
    }
}
