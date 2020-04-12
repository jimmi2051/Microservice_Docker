using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Admin.Models
{
    public class Detail_Order_Adapter
    {
        public int Id { get; set; }

        public int ProductID { get; set; }

        public int Quality { get; set; }

        public float Price { get; set; }

    }
}
