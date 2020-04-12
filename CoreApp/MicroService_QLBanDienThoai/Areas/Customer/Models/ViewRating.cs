using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.Areas.Customer.Models
{
    public class ViewRating
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public int StarRating { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        //Trang thai hien thi rating --- 
        public int Status { get; set; }
    }
}
