using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Customer.Models
{
    public class ViewOrder
    {
        public int Id { get; set; }
        public int AccountID { get; set; }

        public float Amount { get; set; }

        public String Address { get; set; }
        public String Phone { get; set; }
        public String Description { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequireDate { get; set; }
        public int IsPaid { get; set; }

        public int Status { get; set; }

    }
}
