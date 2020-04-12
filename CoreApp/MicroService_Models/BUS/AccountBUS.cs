//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Models.DB;

//namespace Models.BUS
//{
//    public class AccountBUS
//    {
//        private readonly QLBanDienThoaiDBContext context;
//        public AccountBUS()
//        {
//            this.context = new QLBanDienThoaiDBContext();
//        }

//        public AccountBUS(QLBanDienThoaiDBContext context)
//        {
//            this.context = context;
//        }

//        public List<ACCOUNT> GetACCOUNTs()
//        {
//            List<ACCOUNT> list = context.ACCOUNTs.ToList();
//            return list;
//        }



//        //------------------------------------------------------ THEM SUA XOA -----------------------------------------------------------------
//        public string CreateACCOUNT(string AccountID, string AccountName, string Password, string PhoneNumber, string Address, string AccountType)
//        {

//            ACCOUNT account = new ACCOUNT();

//            //----------------------- chuan hoa du lieu ----------------------- 
//            //----------------------- kiem tra ma -----------------------
//            account = context.ACCOUNTs.Where(temp => temp.AccountID == AccountID).SingleOrDefault();
//            if (account != null)
//            {
//                return "Mã Account đã tồn tại";
//            }
//            //----------------------- kiem tra ten -----------------------
//            account = context.ACCOUNTs.Where(temp => temp.AccountName == AccountName).SingleOrDefault();
//            if (account != null)
//            {
//                return "Tên Account đã tồn tại";
//            }
//            //----------------------- them -----------------------
//            account = new ACCOUNT();
//            account.AccountID = AccountID;
//            account.AccountName = AccountName;
//            account.Password = account.Password;
//            account.PhoneNumber = PhoneNumber;
//            account.Address = Address;
//            account.AccountType = AccountType;

//            context.ACCOUNTs.Add(account);
//            context.SaveChanges();

//            return "Thêm thành công";
//        }
//        public string EditACCOUNT(string AccountID, string AccountName, string Password, string PhoneNumber, string Address, string AccountType)
//        {
//            ACCOUNT account = new ACCOUNT();

//            //----------------------- chuan hoa du lieu ----------------------- 
//            //----------------------- kiem tra ma -----------------------
//            account = context.ACCOUNTs.Where(temp => temp.AccountID == AccountID).SingleOrDefault();
//            //----------------------- kiem tra ten -----------------------
//            account = context.ACCOUNTs.Where(temp => temp.AccountName == AccountName).SingleOrDefault();
//            if (account != null)
//            {
//                return "Tên Account này đã tồn tại";
//            }
//            //----------------------- sua -----------------------
//            account = context.ACCOUNTs.Where(temp => temp.AccountID == AccountID).SingleOrDefault();

//            if (AccountID != null)
//            {
//                account.AccountID = AccountID;
//            }
//            if (AccountName != null)
//            {
//                account.AccountName = AccountName;
//            }
//            if (Password != null)
//            {
//                account.Password = Password;
//            }
//            if (PhoneNumber != null)
//            {
//                account.PhoneNumber = PhoneNumber;
//            }
//            if (Address != null)
//            {
//                account.Address = Address;
//            }
//            if (AccountType != null)
//            {
//                account.AccountType = AccountType;
//            }
//            context.SaveChanges();
//            return "Sửa thành công";
//        }

//        public string DeleteACCOUNT(string AccountID)
//        {
//            ACCOUNT account = context.ACCOUNTs.Where(temp => temp.AccountID == AccountID).SingleOrDefault();
//            context.ACCOUNTs.Remove(account);
//            context.SaveChanges();
//            return "Khoá thành công";
//        }

//        public List<ACCOUNT> SearchACCONT(string search)
//        {
//            List<ACCOUNT> list = new List<ACCOUNT>();
//            if (search == null)
//            {
//                list = GetACCOUNTs();
//            }
//            else
//            {
//                list = context.ACCOUNTs.Where(temp => temp.AccountID.Contains(search) || temp.AccountName.Contains(search)).ToList();
//            }
//            return list;
//        }



//    }
//}
