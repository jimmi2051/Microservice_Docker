using Microsoft.EntityFrameworkCore;
using MicroService_QLBanDienThoai.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.BUS
{
    public class LoginBUS
    {
        private readonly QLBanDienThoaiContext context;
        public LoginBUS()
        {
            this.context = new QLBanDienThoaiContext();
        }
        public LoginBUS(QLBanDienThoaiContext context)
        {
            this.context = context;
        }
        public bool getLogin(string username, string password)
        {
            string passMD5= Infrastructure.Encode.md5(password);
            var result = context.Account.Count(x => x.AccountName == username && x.Password == passMD5 && x.AccountType == "admin");
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
