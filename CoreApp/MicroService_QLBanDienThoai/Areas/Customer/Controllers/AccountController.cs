using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.BUS;

namespace MicroService_QLBanDienThoai.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(string item_them_AccountID, string item_them_AccountName, string item_them_Password, string item_them_Email, string item_them_PhoneNumber, string item_them_Address, string item_them_AccountType)
        {
            AccountBUS Account = new AccountBUS();
            string thongbao = Account.CreateAccount(item_them_AccountID, item_them_AccountName, item_them_Password, item_them_PhoneNumber, item_them_Email, item_them_Address, item_them_AccountType);
            return RedirectToAction("Index", "Home", new { thongbao = thongbao });
        }
    }
}