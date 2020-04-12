using System;
using System.Collections.Generic;

namespace MicroService_QLBanDienThoai.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
    }
}
