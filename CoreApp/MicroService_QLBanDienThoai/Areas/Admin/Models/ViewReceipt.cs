using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Admin.Models
{
    public class ViewReceipt
    {
        public int Receipt_ID { get; set; }
        public int AccountID { get; set; }
        public float Amount { get; set; }
        public int State_Receipt { get; set; }
        public DateTime Success_At { get; set; }
        public bool Is_Active { get; set; }
        public bool Archive { get; set; }
    }
}
