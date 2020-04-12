using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.Models;
using Newtonsoft.Json;

namespace Models.BUS
{
    public class AccountBUS
    {
        private readonly QLBanDienThoaiContext context;
        public AccountBUS()
        {
            this.context = new QLBanDienThoaiContext();
        }

        public AccountBUS(QLBanDienThoaiContext context)
        {
            this.context = context;
        }

        public List<Account> GetAccount()
        {
            List<Account> list = context.Account.ToList();
            return list;
        }



        //------------------------------------------------------ THEM SUA XOA -----------------------------------------------------------------
        public string CreateAccount(string AccountID, string AccountName, string Password, string PhoneNumber, string Email, string Address, string AccountType)
        {

            Account Account = new Account();

            //----------------------- chuan hoa du lieu ----------------------- 
            //----------------------- kiem tra ma -----------------------
            //Account = context.Account.Where(temp => temp.AccountId == AccountID).SingleOrDefault();
            //if (Account != null)
            //{
            //    return "Mã Tài Khoản đã tồn tại";
            //}
            //----------------------- kiem tra ten -----------------------
            //Account = context.Account.Where(temp => temp.AccountName == AccountName).SingleOrDefault();
            //if (Account != null)
            //{
            //    return "Tên Account đã tồn tại";
            //}
            //----------------------- them -----------------------
            Account = new Account();
            //Account.AccountId = AccountID;
            Account.AccountName = AccountName;
            Account.Password = Infrastructure.Encode.md5(Password);
            Account.PhoneNumber = PhoneNumber;
            Account.Email = Email;
            Account.Address = Address;
            Account.AccountType = AccountType;

            context.Account.Add(Account);
            context.SaveChanges();


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var obj = new
                {
                    AccountID = Account.AccountId,
                    AccountName = Account.AccountName,
                    AccountType = Account.AccountType,
                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var obj = new
                {
                    AccountID = Account.AccountId,
                    AccountName = Account.AccountName,
                    AccountType = Account.AccountType,
                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var obj = new
                {
                    AccountID = Account.AccountId,
                    AccountName = Account.AccountName,
                    AccountType = Account.AccountType,
                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
            }
            return "Thêm thành công";
        }
        public string EditAccount(string AccountID, string AccountName, string Email, string Password, string PhoneNumber, string Address, string AccountType)
        {
            Account Account = new Account();

            //----------------------- chuan hoa du lieu ----------------------- 
            //----------------------- kiem tra ma -----------------------
            int tempid = Int32.Parse(AccountID);
            Account = context.Account.Where(temp => temp.AccountId == tempid).SingleOrDefault();

            if (AccountName != null)
            {
                Account.AccountName = AccountName;
            }
            if (Password != null)
            {
                Account.Password = Infrastructure.Encode.md5( Password);
            }
            if (PhoneNumber != null)
            {
                Account.PhoneNumber = PhoneNumber;
            }
            if (Address != null)
            {
                Account.Address = Address;
            }
            if (AccountType != null)
            {
                Account.AccountType = AccountType;
            }
            if (Email != null)
            {
                Account.Email = Email;
            }
            context.SaveChanges();
            return "Sửa thành công";
        }

        public string DeleteAccount(string AccountID)
        {
            int tempid = Int32.Parse(AccountID);
            Account Account = context.Account.Where(temp => temp.AccountId == tempid).SingleOrDefault();
            context.Account.Remove(Account);
            context.SaveChanges();
            return "Xoá thành công";
        }

        public List<Account> SearchAccount(string search)
        {
            
            List<Account> list = new List<Account>();
            if (search == null)
            {
                list = GetAccount();
            }
            else
            {
                list = context.Account.Where(temp=> temp.AccountName.Contains(search)).ToList();
            }
            return list;
        }



        public Account CheckTaiKhoan(string tendangnhap, string matkhau, string loainguoidung)
        {
            
            Account taikhoan = context.Account.Where(tk => tk.AccountName == tendangnhap &&
                                                         tk.Password == Infrastructure.Encode.md5(matkhau) &&
                                                         tk.AccountType==loainguoidung)
                                                  .SingleOrDefault();
            return taikhoan;
        }

        public Account GetAccountByName(string name)
        {
            return context.Account.Where(c => c.AccountName == name).FirstOrDefault();
        }
    }
}
